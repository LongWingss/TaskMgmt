namespace TaskMgmt.Services.CustomExceptions
{
    public class InvalidEmailException : Exception
    {
        public InvalidEmailException()
            : base("The email address is invalid.")
        {
        }

        public InvalidEmailException(string message)
            : base(message)
        {
        }

        public InvalidEmailException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
