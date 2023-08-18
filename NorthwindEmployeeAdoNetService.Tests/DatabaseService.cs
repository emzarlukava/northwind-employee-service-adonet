namespace NorthwindEmployeeAdoNetService.Tests;

/// <summary>
/// Provides a service for interacting with an in-memory SQLite database.
/// </summary>
public sealed class DatabaseService : IDisposable
{
    /// <summary>
    /// The connection string for the in-memory SQLite database.
    /// </summary>
    public const string ConnectionString = "Data Source=InMemorySample;Mode=Memory;Cache=Shared";

    private readonly SqliteConnection connection;

    /// <summary>
    /// Initializes a new instance of the <see cref="DatabaseService"/> class.
    /// </summary>
    public DatabaseService()
    {
        this.connection = new SqliteConnection(ConnectionString);
        this.connection.Open();
    }

    /// <summary>
    /// Creates necessary tables in the database.
    /// </summary>
    public void CreateTables()
    {
        using var reader = new StringReader(Properties.Resources.CreateEmployeesTable);
        ExecuteScript(this.connection, reader);
    }

    /// <summary>
    /// Initializes the tables with sample data.
    /// </summary>
    public void InitializeTables()
    {
        using var reader = new StringReader(Properties.Resources.InitializeTable);
        ExecuteScript(this.connection, reader);
    }

    /// <summary>
    /// Executes a SQL command and returns a single scalar value.
    /// </summary>
    /// <typeparam name="T">The type of the scalar result.</typeparam>
    /// <param name="commandText">The SQL command text to execute.</param>
    /// <returns>The scalar result of the SQL command.</returns>
    public T ExecuteScalar<T>(string commandText)
    {
        using var command = this.connection.CreateCommand();
        command.CommandText = commandText;
        object? result = command.ExecuteScalar();
        return result is not null ? (T)result : throw new SqlScriptException($"SQL statement failed: {command}.");
    }

    /// <summary>
    /// Executes a non-query SQL command and returns the number of affected rows.
    /// </summary>
    /// <param name="commandText">The SQL command text to execute.</param>
    /// <returns>The number of affected rows.</returns>
    public int ExecuteNonQuery(string commandText)
    {
        using var command = this.connection.CreateCommand();
        command.CommandText = commandText;
        return command.ExecuteNonQuery();
    }

    /// <inheritdoc />
    public void Dispose()
    {
        this.connection.Dispose();
    }

    private static void ExecuteScript(SqliteConnection connection, TextReader reader)
    {
        using var transaction = connection.BeginTransaction();

        try
        {
            int lineNumber = 1;
            string? line;

            while ((line = reader.ReadLine()) is not null)
            {
                lineNumber++;

                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                try
                {
                    using var command = new SqliteCommand(line, connection, transaction);
                    _ = command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw new SqlScriptException(
                        $"Exception during executing an SQL command on line {lineNumber}: \"{line}\".", e);
                }
            }

            transaction.Commit();
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
        }
    }
}
