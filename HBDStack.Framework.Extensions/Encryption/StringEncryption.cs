using System.Security.Cryptography;
using System.Text;

namespace HBDStack.Framework.Extensions.Encryption;

public static class StringEncryption
{
    private static void ValidateKey(string keyString)
    {
        if (keyString.Length != 32) throw new ArgumentException($"The lenght of {nameof(keyString)} must be 32");
    }

    public static string EncryptWith(this string planText, string keyString)
    {
        ValidateKey(keyString);
        var key = Encoding.UTF8.GetBytes(keyString);

        using var aesAlg = Aes.Create();
        using var encryptor = aesAlg.CreateEncryptor(key, aesAlg.IV);
        using var msEncrypt = new MemoryStream();
        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        using (var swEncrypt = new StreamWriter(csEncrypt))
            swEncrypt.Write(planText);

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