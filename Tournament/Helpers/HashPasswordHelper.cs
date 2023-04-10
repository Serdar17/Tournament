using System.Security.Cryptography;
using System.Text;

namespace Tournament.Helpers;

public static class HashPasswordHelper
{
    public static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        var hash = BitConverter.ToString(bytes).Replace("-", "").ToLower();
        return hash;
    }
}