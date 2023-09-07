namespace Domain.Errors;

public class BadArgumentException : Exception
{
    public BadArgumentException(string msg) : base(msg){}
}