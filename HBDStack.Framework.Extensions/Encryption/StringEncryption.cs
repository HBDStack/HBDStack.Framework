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

        var buffer = new Span<byte>(new byte[@this.Length]);
        return Convert.TryFromBase64String(@this, buffer, out _);
        // var regex = new Regex("^([A-Za-z0-9+/]{4})*([A-Za-z0-9+/]{4}|[A-Za-z0-9+/]{3}=|[A-Za-z0-9+/]{2}==)$");
        // return regex.IsMatch(@this);
    }

    public static string EncryptWithBase64(this string plainText) =>
        Encoding.UTF8.GetBytes(plainText).EncryptWithBase64();

    public static string EncryptWithBase64(this byte[] plainTextBytes) => Convert.ToBase64String(plainTextBytes);

    public static string DecryptWithBase64(this string encryptedText)
    {
        var base64EncodedBytes = Convert.FromBase64String(encryptedText);
        return Encoding.UTF8.GetString(base64EncodedBytes);
    }

    public static string GenerateAesKey()
    {
        using var aes = Aes.Create();
        var k = aes.Key;
        var iv = aes.IV;
        return $"{k.EncryptWithBase64()}:{iv.EncryptWithBase64()}".EncryptWithBase64();
    }

    private static Aes CreateAes(string keyString)
    {
        var keys = keyString.DecryptWithBase64().Split(":");
        if (keys.Length != 2) throw new ArgumentException("Invalid", nameof(keyString));

        var k = Convert.FromBase64String(keys[0]);
        var iv = Convert.FromBase64String(keys[1]);

        var aes = Aes.Create();
        aes.Key = k;
        aes.IV = iv;
        
        return aes;
    }

    /// <summary>
    /// Encrypt With Aes
    /// </summary>
    /// <param name="plainText"></param>
    /// <param name="keyString">The key must be created by <see cref="GenerateAesKey"/></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static string EncryptWithAes(this string plainText, string keyString)
    {
        // Check arguments.
        if (plainText is not { Length: > 0 })
            throw new ArgumentNullException(nameof(plainText));

        // Create an Aes object
        // with the specified key and IV.
        using var aesAlg = CreateAes(keyString);
        // Create an encryptor to perform the stream transform.
        var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        // Create the streams used for encryption.
        using var msEncrypt = new MemoryStream();
        using var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
        using (var swEncrypt = new StreamWriter(csEncrypt))
        {
            //Write all data to the stream.
            swEncrypt.Write(plainText);
        }

        var encrypted = msEncrypt.ToArray();
        return encrypted.EncryptWithBase64();
    }

    /// <summary>
    /// Decrypt With Aes
    /// </summary>
    /// <param name="cipherText"></param>
    /// <param name="keyString">The key must be created by <see cref="GenerateAesKey"/></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static string DecryptWithAes(this string cipherText, string keyString)
    {
        // Check arguments.
        if (cipherText is not { Length: > 0 })
            throw new ArgumentNullException(nameof(cipherText));

        // Create an Aes object with the specified key and IV.
        using var aesAlg = CreateAes(keyString);
        // Create a decryptor to perform the stream transform.
        var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        // Create the streams used for decryption.
        using var msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText));
        using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
        using var srDecrypt = new StreamReader(csDecrypt);

        return srDecrypt.ReadToEnd();
    }
}