using System.Security.Cryptography;
using FluentAssertions;
using HBDStack.Framework.Extensions.Encryption;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HBDStack.Framework.Extensions.Tests;

[TestClass]
public class StringEncryptionTests
{
    private const string Key = "8!DQQx@5.~#j_JM>ri.vAF2A.*q_.zYx";

    [TestMethod]
    public void EncryptWithPassword_Test()
    {
        var s = "Steven Hoang";
        var e = s.EncryptWith(Key);
        e.Should().NotBe(s);
        e.IsEncrypted().Should().BeTrue();
        
        var a = () => e.DecryptWith("03d!C3h^Z#XH52Ek8E?sNFcEEAP2z.?}");
        a.Should().Throw<CryptographicException>();

        e.DecryptWith(Key).Should().Be(s);
    }
    
    [TestMethod]
    public void EncryptWithBase64_Test()
    {
        var s = "Steven Hoang";
        var e = s.EncryptWithBase64();
        e.Should().NotBe(s);
        e.IsEncrypted().Should().BeTrue();

        e.DecryptWithBase64().Should().Be(s);
    }
}