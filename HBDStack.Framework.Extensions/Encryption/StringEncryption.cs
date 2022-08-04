using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Text.Unicode;

namespace HBDStack.Framework.Extensions.Encryption;

public static class StringEncryption
{
    public static bool IsEncrypted(this string @this)
    {
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
    
    private static void ValidateKey(string keyString)
    {
        if (keyString.Length != 32) throw new ArgumentException($"The lenght of {nameof(keyString)} must be 32");
    }

    public static string EncryptWith(this string plainText, string keyString)
    {
        ValidateKey(keyString);
        var key = Encoding.UTF8.GetBytes(keyString);

        using var aesAlg = Aes.Create();
        using var encryptor = aesAlg.CreateEncryptor(key, aesAlg.IV);
        using var msEncrypt = new MemoryStream();
        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        using (var swEncrypt = new StreamWriter(csEncrypt))
            swEncrypt.Write(plainText);

        var iv = aesAlg.IV;
        var decryptedContent = msEncrypt.ToArray();
        var result = new byte[iv.Length + decryptedContent.Length];

        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
        Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

        return Convert.ToBase64String(result);
    }

    public static string DecryptWith(this string encryptedText, string keyString)
    {
        ValidateKey(keyString);
        var fullCipher = Convert.FromBase64String(encryptedText);

        var iv = new byte[16];
        var cipher = new byte[16];

        Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
        Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, iv.Length);
        var key = Encoding.UTF8.GetBytes(keyString);

        using var aesAlg = Aes.Create();
        using var decrypt = aesAlg.CreateDecryptor(key, iv);
        using var msDecrypt = new MemoryStream(cipher);
        using var csDecrypt = new CryptoStream(msDecrypt, decrypt, CryptoStreamMode.Read);
        using var srDecrypt = new StreamReader(csDecrypt);
        var result = srDecrypt.ReadToEnd();

        return result;
    }
}