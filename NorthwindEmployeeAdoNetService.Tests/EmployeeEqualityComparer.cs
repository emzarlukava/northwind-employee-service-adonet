namespace NorthwindEmployeeAdoNetService.Tests;

/// <summary>
/// Provides an equality comparer for the <see cref="Employee"/> class using all properties.
/// </summary>
internal class EmployeeEqualityComparer : EqualityComparer<Employee>
{
    /// <summary>
    /// Determines whether two employee objects are equal using all properties for comparison.
    /// </summary>
    /// <param name="x">The first employee to compare.</param>
    /// <param name="y">The second employee to compare.</param>
    /// <returns>
    /// <c>true</c> if the specified employee objects are equal; otherwise, <c>false</c>.
    /// </returns>
    public override bool Equals(Employee? x, Employee? y)
    {
        if (ReferenceEquals(x, y))
        {
            return true;
        }

        if (x is null || y is null)
        {
            return false;
        }

        return x.Id == y.Id &&
               x.FirstName == y.FirstName &&
               x.LastName == y.LastName &&
               x.Title == y.Title &&
               x.TitleOfCourtesy == y.TitleOfCourtesy &&
               x.BirthDate == y.BirthDate &&
               x.HireDate == y.HireDate &&
               x.Address == y.Address &&
               x.City == y.City &&
               x.Region == y.Region &&
               x.PostalCode == y.PostalCode &&
               x.Country == y.Country &&
               x.HomePhone == y.HomePhone &&
               x.Extension == y.Extension &&
               x.Notes == y.Notes &&
               x.ReportsTo == y.ReportsTo &&
               x.PhotoPath == y.PhotoPath;
    }

    /// <summary>
    /// Returns a hash code for the specified employee object using Id, FirstName, LastName and Title properties.
    /// </summary>
    /// <param name="obj">The employee object for which to get a hash code.</param>
    /// <returns>A hash code for the specified employee object.</returns>
    public override int GetHashCode(Employee obj)
    {
        return HashCode.Combine(obj.Id, obj.FirstName, obj.LastName, obj.Title);
    }
}