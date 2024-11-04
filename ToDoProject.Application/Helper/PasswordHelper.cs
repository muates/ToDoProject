namespace ToDoProject.Application.Helper;

public static class PasswordHelper
{
    public static (byte[] passwordHash, byte[] passwordSalt) CreatePasswordHash(string password)
    {
        using var hmac = new System.Security.Cryptography.HMACSHA512();
        var passwordSalt = hmac.Key;
        var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        return (passwordHash, passwordSalt);
    }
    
    public static bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt);
        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(passwordHash);
    }
}