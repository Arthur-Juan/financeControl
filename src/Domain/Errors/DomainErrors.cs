
using System.Security.Cryptography.X509Certificates;

namespace Domain.Errors;

public static class DomainErrors
{
    public static class Shared
    {
        public static string RequiredFiled(string field)
        {
            return $"The field {field} is required";
        }
    }

    public static class User
    {
        public const string InvalidEmail = "Invalid Email";
        public const  string PasswordDontMatch = "Password dont match";
        public const string EmailInUse = "Email already in use";
        public const string InvalidCredentials = "Invalid Credentials";
    }
    
    public static class Department
    {
        public const string UserNotFound = "User not found";
        public const string AlreadyExists = "A department with this name already exists";
    }
}