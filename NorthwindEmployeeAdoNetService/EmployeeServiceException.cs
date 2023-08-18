using System.Runtime.Serialization;

namespace NorthwindEmployeeAdoNetService;

/// <summary>
/// Represents an exception specific to the employee service.
/// </summary>
[Serializable]
public class EmployeeServiceException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EmployeeServiceException"/> class.
    /// </summary>
    public EmployeeServiceException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EmployeeServiceException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public EmployeeServiceException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EmployeeServiceException"/> class with a specified error message
    /// and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public EmployeeServiceException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EmployeeServiceException"/> class with serialized data.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
    /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
    protected EmployeeServiceException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}