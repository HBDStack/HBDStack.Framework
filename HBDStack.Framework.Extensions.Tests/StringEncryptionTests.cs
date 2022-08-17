using System;
using System.Security.Cryptography;
using FluentAssertions;
using HBDStack.Framework.Extensions.Encryption;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HBDStack.Framework.Extensions.Tests;

[TestClass]
public class StringEncryptionTests
{
    [TestMethod]
    public void EncryptWithBase64_Test()
    {
        var s = "Steven Hoang";
        var e = s.EncryptWithBase64();
        e.Should().Be("U3RldmVuIEhvYW5n");

        e.IsEncrypted().Should().BeTrue();
        e.DecryptWithBase64().Should().Be(s);
    }

    [TestMethod]
    public void IsEncryptBase64_Test()
    {
        "8080".IsEncrypted().Should().BeFalse();
        "Duy".IsEncrypted().Should().BeFalse();
        "Error".IsEncrypted().Should().BeFalse();
        "Information".IsEncrypted().Should().BeFalse();
        "None".IsEncrypted().Should().BeFalse();
        "udfclientdev".IsEncrypted().Should().BeFalse();
        
        bool.FalseString.ToLower().IsEncrypted().Should().BeFalse();
        bool.TrueString.ToLower().IsEncrypted().Should().BeFalse();
        string.Empty.IsEncrypted().Should().BeFalse();

        "U3RldmVuIEhvYW5n".IsEncrypted().Should().BeTrue();
        "Lg==".IsEncrypted().Should().BeTrue();
    }

    [TestMethod]
    public void Aes_Test()
    {
        var key = StringEncryption.GenerateAesKey();

        var enc = "Hoang Bao Duy".EncryptWithAes(key);
        enc.IsEncrypted().Should().BeTrue();

        enc.DecryptWithAes(key).Should().Be("Hoang Bao Duy");
    }

    [TestMethod]
    public void Aes_Failed_Test()
    {
        var key = StringEncryption.GenerateAesKey();

        var enc = "Hoang Bao Duy".EncryptWithAes(key);
        //enc.IsEncrypted().Should().BeTrue();
        //enc.DecryptWithBase64().Should().NotBe("Hoang Bao Duy");
    }
}