using System.Diagnostics;

namespace NorthwindEmployeeAdoNetService;

/// <summary>
/// Represents an employee in the organization.
/// </summary>
[DebuggerDisplay("{Id}, {FirstName}, {LastName}")]
public class Employee
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Employee"/> class.
    /// </summary>
    /// <param name="id">The unique identifier of the employee.</param>
    public Employee(long id)
    {
        this.Id = id;
    }

    /// <summary>
    /// Gets the unique identifier of the employee.
    /// </summary>
    public long Id { get; }

    /// <summary>
    /// Gets or sets the first name of the employee.
    /// </summary>
    public string FirstName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the last name of the employee.
    /// </summary>
    public string LastName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the title of the employee.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets the courtesy title of the employee.
    /// </summary>
    public string? TitleOfCourtesy { get; set; }

    /// <summary>
    /// Gets or sets the birth date of the employee.
    /// </summary>
    public DateTime? BirthDate { get; set; }

    /// <summary>
    /// Gets or sets the hire date of the employee.
    /// </summary>
    public DateTime? HireDate { get; set; }

    /// <summary>
    /// Gets or sets the address of the employee.
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Gets or sets the city where the employee lives.
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// Gets or sets the region or state where the employee lives.
    /// </summary>
    public string? Region { get; set; }

    /// <summary>
    /// Gets or sets the postal code of the employee's address.
    /// </summary>
    public string? PostalCode { get; set; }

    /// <summary>
    /// Gets or sets the country where the employee lives.
    /// </summary>
    public string? Country { get; set; }

    /// <summary>
    /// Gets or sets the home phone number of the employee.
    /// </summary>
    public string? HomePhone { get; set; }

    /// <summary>
    /// Gets or sets the extension number for contacting the employee.
    /// </summary>
    public string? Extension { get; set; }

    /// <summary>
    /// Gets or sets additional notes or information about the employee.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the employee's supervisor.
    /// </summary>
    public long? ReportsTo { get; set; }

    /// <summary>
    /// Gets or sets the file path to the employee's photo.
    /// </summary>
    public string? PhotoPath { get; set; }
}