namespace NorthwindEmployeeAdoNetService.Tests;

internal class EmployeeAdoNetServiceReadOperationsTests : IDisposable
{
    private DatabaseService databaseService = default!;
    private EmployeeAdoNetService adoNetService = default!;

    [OneTimeSetUp]
    public void Init()
    {
        this.databaseService = new DatabaseService();
        this.databaseService.CreateTables();
        this.databaseService.InitializeTables();
        this.adoNetService = new EmployeeAdoNetService(SqliteFactory.Instance, DatabaseService.ConnectionString);
    }

    [OneTimeTearDown]
    public void Cleanup() => this.Dispose();

    [Test]
    public void GetEmployees_ReturnsEmployeeList()
    {
        var actual = EmployeesDataSource.GetEmployees.ToList();
        IList<Employee> expected = this.adoNetService.GetEmployees();
        Assert.That(actual.Count, Is.EqualTo(expected.Count));
        var comparer = new EmployeeEqualityComparer();
        for (int i = 0; i < expected.Count; i++)
        {
            Assert.That(actual[i].Id, Is.EqualTo(expected[i].Id));
        }
    }

    [Test]
    public void GetEmployee_EmployeeIsNotExist_ThrowsRepositoryException()
    {
        _ = Assert.Throws<EmployeeServiceException>(() => this.adoNetService.GetEmployee(employeeId: 0));
    }

    [TestCaseSource(typeof(EmployeesDataSource), nameof(EmployeesDataSource.GetEmployees))]
    public void GetEmployee_EmployeeIsExist_ReturnsEmployee(Employee employee)
    {
        Employee actualEmployee = this.adoNetService.GetEmployee(employee.Id);
        Assert.That(actualEmployee, Is.Not.Null);
        Assert.That(employee.Id, Is.EqualTo(actualEmployee.Id));
    }

    public void Dispose()
    {
        this.databaseService.Dispose();
    }
}