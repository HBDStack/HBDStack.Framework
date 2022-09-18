using System.Security.Cryptography;
using System.Text;

namespace HBDStack.Framework.Extensions.Encryption;

public static class Hashing
{
    public static string HashCmd5(this string value, string key)
    {
        var hmac = new HMACMD5(Encoding.UTF8.GetBytes(key));
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(value));

        var result = new StringBuilder();
        foreach (var t in hash)
            result.Append(t.ToString("X"));
        
        return result.ToString();
    }
}