namespace Domain.Errors;

public class ForbiddenException : Exception
{
    public ForbiddenException(string msg) : base(msg){}
}