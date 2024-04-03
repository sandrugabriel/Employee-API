namespace EmployeeAPI.Exceptions
{
    public class InvalidSalary : Exception
    {
        public InvalidSalary(string? message):base(message) { }
    }
}
