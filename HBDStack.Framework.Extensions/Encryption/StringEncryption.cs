using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Text.Unicode;

namespace HBDStack.Framework.Extensions.Encryption;

public static class StringEncryption
{
    public static bool IsEncrypted(this string @this)
    {
        if (string.IsNullOrWhiteSpace(@this)) return false;
        if (bool.FalseString.Equals(@this, StringComparison.CurrentCultureIgnoreCase)) return false;
        if (bool.TrueString.Equals(@this, StringComparison.CurrentCultureIgnoreCase)) return false;
        
        var regex = new Regex("^([A-Za-z0-9+/]{4})*([A-Za-z0-9+/]{4}|[A-Za-z0-9+/]{3}=|[A-Za-z0-9+/]{2}==)$");
        return regex.IsMatch(@this);
    }

    public static string EncryptWithBase64(this string plainText)
    {
        var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        return Convert.ToBase64String(plainTextBytes);
    }
    
    public static string DecryptWithBase64(this string encryptedText)
    {
        var base64EncodedBytes = Convert.FromBase64String(encryptedText);
        return Encoding.UTF8.GetString(base64EncodedBytes);
    }
}