#pragma warning disable

using Microsoft.Data.Sqlite;
using System;

namespace NorthwindEmployeeAdoNetService;

/// <summary>
/// A service for interacting with the "Employees" table using ADO.NET.
/// </summary>
public sealed class EmployeeAdoNetService
{
    private readonly DbProviderFactory dbProviderFactory;
    private readonly string connectionString;
    /// <summary>
    /// Initializes a new instance of the <see cref="EmployeeAdoNetService"/> class.
    /// </summary>
    /// <param name="dbFactory">The database provider factory used to create database connection and command instances.</param>
    /// <param name="connectionString">The connection string used to establish a database connection.</param>
    /// <exception cref="ArgumentNullException">Thrown when either <paramref name="dbFactory"/> or <paramref name="connectionString"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="connectionString"/> is empty or contains only white-space characters.</exception>
    public EmployeeAdoNetService(DbProviderFactory dbFactory, string connectionString)
    {
        if (dbFactory is null )
        {
            throw new ArgumentNullException(nameof(dbFactory));
        }

        if(connectionString is null)
        {
            throw new ArgumentNullException(nameof(connectionString));
        }

        if (String.IsNullOrEmpty(connectionString) || String.IsNullOrWhiteSpace(connectionString))
        {
            throw new ArgumentException("Connection string cannot be empty or whitespace.", nameof(connectionString));
        }

        dbProviderFactory = dbFactory;
        this.connectionString = connectionString;
    }

    /// <summary>
    /// Retrieves a list of all employees from the Employees table of the database.
    /// </summary>
    /// <returns>A list of Employee objects representing the retrieved employees.</returns>
    public IList<Employee> GetEmployees()
    {
        List<Employee> employees = new List<Employee>();

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Employees";

            using(var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Employee employee = new Employee(reader.GetInt64(0));
                    employee.LastName = reader.GetString(1);
                    employee.FirstName = reader.GetString(2);
                    employee.Title = reader.GetString(3);
                    employee.TitleOfCourtesy = reader.GetString(4);
                    employee.BirthDate = reader.GetDateTime(5);
                    employee.HireDate = reader.GetDateTime(6);
                    employee.Address = reader.GetString(7);
                    employee.City = reader.GetString(8);
                    if (!reader.IsDBNull(9))
                        employee.Region = reader.GetString(9);
                    employee.PostalCode = reader.GetString(10);
                    employee.Country = reader.GetString(11);
                    employee.HomePhone = reader.GetString(12);
                    employee.Extension = reader.GetString(13);
                    employee.Notes = reader.GetString(14);
                    if (!reader.IsDBNull(15))
                        employee.ReportsTo = reader.GetInt64(15);
                    employee.PhotoPath = reader.GetString(16);

                    employees.Add(employee);
                }
            }
        }

        return employees;
    }

    /// <summary>
    /// Retrieves an employee with the specified employee ID.
    /// </summary>
    /// <param name="employeeId">The ID of the employee to retrieve.</param>
    /// <returns>The retrieved an <see cref="Employee"/> instance.</returns>
    /// <exception cref="EmployeeServiceException">Thrown if the employee is not found.</exception>
    public Employee GetEmployee(long employeeId)
    {
        Employee employee = null;

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
        @"
        SELECT *
        FROM Employees
        WHERE employeeId = $id
    ";

            command.Parameters.AddWithValue("$id", employeeId);

            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    employee = new Employee(reader.GetInt64(0));
                    employee.LastName = reader.GetString(1);
                    employee.FirstName = reader.GetString(2);
                    employee.Title = reader.GetString(3);
                    employee.TitleOfCourtesy = reader.GetString(4);
                    employee.BirthDate = reader.GetDateTime(5);
                    employee.HireDate = reader.GetDateTime(6);
                    employee.Address = reader.GetString(7);
                    employee.City = reader.GetString(8);
                    if (!reader.IsDBNull(9))
                        employee.Region = reader.GetString(9);
                    employee.PostalCode = reader.GetString(10);
                    employee.Country = reader.GetString(11);
                    employee.HomePhone = reader.GetString(12);
                    employee.Extension = reader.GetString(13);
                    employee.Notes = reader.GetString(14);
                    if (!reader.IsDBNull(15))
                        employee.ReportsTo = reader.GetInt64(15);
                    employee.PhotoPath = reader.GetString(16);
                }
                else
                    throw new EmployeeServiceException("employee not found");
            }
        }

        return employee;
    }
    /// <summary>
    /// Adds a new employee to Employee table of the database.
    /// </summary>
    /// <param name="employee">The  <see cref="Employee"/> object containing the employee's information.</param>
    /// <returns>The ID of the newly added employee.</returns>
    /// <exception cref="EmployeeServiceException">Thrown when an error occurs while adding the employee.</exception>
    public long AddEmployee(Employee employee)
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
        @"
        INSERT INTO Employees (LastName, FirstName, Title, TitleOfCourtesy, BirthDate, HireDate, Address, City, Region, PostalCode, Country, HomePhone, Extension, Notes, ReportsTo, PhotoPath)
        VALUES ($lastName, $firstName, $title, $titleOfCourtesy, $birthDate, $hireDate, $address, $city, $region, $postalCode, $country, $homePhone, $extension, $notes, $reportsTo, $photoPath);
        SELECT last_insert_rowid();
    ";

            command.Parameters.AddWithValue("$lastName", employee.LastName);
            command.Parameters.AddWithValue("$firstName", employee.FirstName);
            command.Parameters.AddWithValue("$title", employee.Title);
            command.Parameters.AddWithValue("$titleOfCourtesy", employee.TitleOfCourtesy);
            command.Parameters.AddWithValue("$birthDate", employee.BirthDate);
            command.Parameters.AddWithValue("$hireDate", employee.HireDate);
            command.Parameters.AddWithValue("$address", employee.Address);
            command.Parameters.AddWithValue("$city", employee.City);
            command.Parameters.AddWithValue("$region", (object)employee.Region ?? DBNull.Value);
            command.Parameters.AddWithValue("$postalCode", employee.PostalCode);
            command.Parameters.AddWithValue("$country", employee.Country);
            command.Parameters.AddWithValue("$homePhone", employee.HomePhone);
            command.Parameters.AddWithValue("$extension", employee.Extension);
            command.Parameters.AddWithValue("$notes", employee.Notes);
            command.Parameters.AddWithValue("$reportsTo", (object)employee.ReportsTo ?? DBNull.Value);
            command.Parameters.AddWithValue("$photoPath", employee.PhotoPath);

            // ExecuteScalar to get the ID of the newly inserted employee
            long newEmployeeId = (long)command.ExecuteScalar();

            return newEmployeeId;
        }
    }

    /// <summary>
    /// Removes an employee from the the Employee table of the database based on the provided employee ID.
    /// </summary>
    /// <param name="employeeId">The ID of the employee to remove.</param>
    /// <exception cref="EmployeeServiceException"> Thrown when an error occurs while attempting to remove the employee.</exception>
    public void RemoveEmployee(long employeeId)
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
        @"
        DELETE FROM Employees
        WHERE EmployeeId = $employeeId
    ";

            command.Parameters.AddWithValue("$employeeId", employeeId);

            int rowsAffected = command.ExecuteNonQuery();
        }
    }

    /// <summary>
    /// Updates an employee record in the Employee table of the database.
    /// </summary>
    /// <param name="employee">The employee object containing updated information.</param>
    /// <exception cref="EmployeeServiceException">Thrown when there is an issue updating the employee record.</exception>
    public void UpdateEmployee(Employee employee)
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
        @"
        UPDATE Employees
        SET LastName = $lastName,
            FirstName = $firstName,
            Title = $title,
            TitleOfCourtesy = $titleOfCourtesy,
            BirthDate = $birthDate,
            HireDate = $hireDate,
            Address = $address,
            City = $city,
            Region = $region,
            PostalCode = $postalCode,
            Country = $country,
            HomePhone = $homePhone,
            Extension = $extension,
            Notes = $notes,
            ReportsTo = $reportsTo,
            PhotoPath = $photoPath
        WHERE EmployeeId = $id
    ";

            command.Parameters.AddWithValue("$lastName", employee.LastName);
            command.Parameters.AddWithValue("$firstName", employee.FirstName);
            command.Parameters.AddWithValue("$title", employee.Title);
            command.Parameters.AddWithValue("$titleOfCourtesy", employee.TitleOfCourtesy);
            command.Parameters.AddWithValue("$birthDate", employee.BirthDate);
            command.Parameters.AddWithValue("$hireDate", employee.HireDate);
            command.Parameters.AddWithValue("$address", employee.Address);
            command.Parameters.AddWithValue("$city", employee.City);
            command.Parameters.AddWithValue("$region", (object)employee.Region ?? DBNull.Value);
            command.Parameters.AddWithValue("$postalCode", employee.PostalCode);
            command.Parameters.AddWithValue("$country", employee.Country);
            command.Parameters.AddWithValue("$homePhone", employee.HomePhone);
            command.Parameters.AddWithValue("$extension", employee.Extension);
            command.Parameters.AddWithValue("$notes", employee.Notes);
            command.Parameters.AddWithValue("$reportsTo", (object)employee.ReportsTo ?? DBNull.Value);
            command.Parameters.AddWithValue("$photoPath", employee.PhotoPath);
            command.Parameters.AddWithValue("$id", employee.Id); // Assuming Id is the property representing the employee's ID

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected == 0)
            {
                // Throw exception or handle situation where no employee was found with the given ID
                throw new EmployeeServiceException("No employee found with the provided ID.");
            }
        }
    }
}