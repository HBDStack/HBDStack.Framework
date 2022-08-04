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
        "Duy".IsEncrypted().Should().BeFalse();
        Boolean.FalseString.ToLower().IsEncrypted().Should().BeFalse();
        Boolean.TrueString.ToLower().IsEncrypted().Should().BeFalse();
        string.Empty.IsEncrypted().Should().BeFalse();

        "U3RldmVuIEhvYW5n".IsEncrypted().Should().BeTrue();
    }
}