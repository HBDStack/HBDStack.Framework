﻿#region using

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

#region

#endregion using

#endregion

namespace HBDStack.Framework.Extensions.Tests;

[TestClass]
public class StringExtensionsTests
{
    [TestMethod]
    public void IsNumberTests()
    {
        "123".IsNumber().Should().BeTrue();
        "123,456".IsNumber().Should().BeTrue();
        "123.456".IsNumber().Should().BeTrue();
        "123,A.456".IsNumber().Should().BeFalse();
    }
    
    [TestMethod]
    public void IsStringOrValueType_Tests()
    {
        typeof(string).IsStringOrValueType().Should().BeTrue();
        typeof(int).IsStringOrValueType().Should().BeTrue();
        typeof(long).IsStringOrValueType().Should().BeTrue();
        typeof(double).IsStringOrValueType().Should().BeTrue();
        typeof(decimal).IsStringOrValueType().Should().BeTrue();
        typeof(byte).IsStringOrValueType().Should().BeTrue();
        
        typeof(int?).IsStringOrValueType().Should().BeTrue();
        typeof(long?).IsStringOrValueType().Should().BeTrue();
        typeof(double?).IsStringOrValueType().Should().BeTrue();
        typeof(decimal?).IsStringOrValueType().Should().BeTrue();
        typeof(byte?).IsStringOrValueType().Should().BeTrue();
        
        typeof(object).IsStringOrValueType().Should().BeFalse();
        typeof(ClassCleanupAttribute).IsStringOrValueType().Should().BeFalse();
    }
    
    // [TestMethod]
    // public void ConsolidateWordsTest()
    // {
    //     Assert.AreEqual("HoangBaoDuy".ToDisplayWords(), "Hoang Bao Duy");
    //
    //     Assert.AreEqual("HelloWorld".ToDisplayWords(), "Hello World");
    //     Assert.AreEqual("Hello World".ToDisplayWords(), "Hello World");
    //     Assert.AreEqual("Hello".ToDisplayWords(), "Hello");
    //
    //     Assert.AreEqual("Hello123ABC".ToDisplayWords(), "Hello 123 ABC");
    // }
    //
    // [TestMethod]
    // public void ContainsAnyIgnoreCaseTest()
    // {
    //     Assert.IsTrue("Hoang Bao Duy".AnyOfIgnoreCase("bao"));
    //     Assert.IsFalse("Hoang Bao Duy".AnyOfIgnoreCase("aaa"));
    // }
    //
    // [TestMethod]
    // public void ContainsAnyTest()
    // {
    //     Assert.IsTrue("Hoang Bao Duy".AnyOf("Bao"));
    //     Assert.IsFalse("Hoang Bao Duy".AnyOf("duy"));
    // }
    //
    // [TestMethod]
    // public void ContainsIgnoreCaseTest()
    // {
    //     Assert.IsTrue(new[] { "123", "ABC", "aaa" }.AnyIgnoreCase("AAA"));
    //     Assert.IsFalse(new string[] { }.AnyIgnoreCase("AAA"));
    // }
    //
    // [TestMethod]
    // public void ContainsItemIgnoreCaseTest()
    // {
    //     var list = new[] { "Hoang", "bAo", "duy" };
    //
    //     Assert.IsTrue(list.Contains("Hoang"));
    //     Assert.IsTrue(list.Contains("bAo"));
    //     Assert.IsTrue(list.Contains("duy"));
    //
    //     Assert.IsFalse(list.Contains("hoang"));
    //     Assert.IsFalse(list.Contains("bao"));
    //     Assert.IsFalse(list.Contains("dUy"));
    //
    //     Assert.IsTrue(list.AnyIgnoreCase("hoang"));
    //     Assert.IsTrue(list.AnyIgnoreCase("bao"));
    //     Assert.IsTrue(list.AnyIgnoreCase("DUY"));
    //
    //     Assert.IsFalse(list.AnyIgnoreCase("A"));
    //     Assert.IsFalse(list.AnyIgnoreCase("B"));
    //     Assert.IsFalse(list.AnyIgnoreCase("C"));
    //
    //     Assert.IsFalse(((ICollection<string>)null).AnyIgnoreCase("A"));
    //     Assert.IsFalse(new string[] { }.AnyIgnoreCase("B"));
    // }
    //
    // [TestMethod]
    // public void ExtractDatesTest()
    // {
    //     Assert.AreEqual("Testing Date Time 2016/07/08".ExtractDates("yyyy/MM/dd").First(),
    //         new DateTime(2016, 07, 08));
    //     Assert.AreEqual("Testing Date Time 08/Jul/16".ExtractDates("dd/MMM/yy").First(),
    //         new DateTime(2016, 07, 08));
    //
    //     Assert.AreEqual("Testing Date Time 2016/07/08 12:24:00".ExtractDates("yyyy/MM/dd hh:mm:ss").First(),
    //         new DateTime(2016, 07, 08, 0, 24, 0));
    //     Assert.AreEqual("Testing Date Time 08/Jul/16_002412".ExtractDates("dd/MMM/yy_hhmmss").First(),
    //         new DateTime(2016, 07, 08, 0, 24, 12));
    // }

    // [TestMethod]
    // [TestCategory("Fw.Extensions")]
    // public void GetMd5HashCodeTest()
    // {
    //     Assert.IsTrue("".GetMd5HashCode() == "");
    // }
    // [TestMethod]
    // [TestCategory("Fw.Extensions")]
    // public void IsEmail_Tests()
    // {
    //     "duy@123.com".IsEmail().Should().BeTrue();
    //     "duy@123.".IsEmail().Should().BeFalse();
    // }
    //
    // [TestMethod]
    // [TestCategory("Fw.Extensions")]
    // public void IsInIgnoreCaseTest()
    // {
    //     Assert.IsTrue("abc".AnyOfIgnoreCase("ABC", "AAA"));
    //     Assert.IsFalse("a".AnyOfIgnoreCase("ABC", "AAA"));
    // }
    //
    // [TestMethod]
    // [TestCategory("Fw.Extensions")]
    // public void IsInTest()
    // {
    //     Assert.IsFalse("".AnyOf("123", "456"));
    //     Assert.IsFalse("123".AnyOf());
    //     Assert.IsFalse("abc".AnyOf("ABC", "AAA"));
    // }
    //
    // [TestMethod]
    // public void ReplaceIgnoreCaseTest()
    // {
    //     Assert.AreEqual("Hoang Bao Duy".ReplaceIgnoreCase("duy", "Test"), "Hoang Bao Test");
    //     Assert.AreEqual("Hoang Bao Duy".ReplaceIgnoreCase("BAO", "TEST"), "Hoang TEST Duy");
    //     Assert.IsNull(((string)null).ReplaceIgnoreCase("BAO", "TEST"));
    // }
    //
    // [TestMethod]
    // public void SingleString_ContainsIgnoreCaseTest()
    // {
    //     Assert.IsTrue("Hoang Bao Duy".ContainsIgnoreCase("bao"));
    //     Assert.IsFalse("Hoang Bao Duy".ContainsIgnoreCase("aaa"));
    // }
    //
    // [TestMethod]
    // public void SplitWords_With_MultiLine_Test()
    // {
    //     var splits = @"Hoang
    // Bao
    // Duy".SplitWords();
    //
    //     Assert.AreEqual(3, splits.Length);
    // }
    //
    // [TestMethod]
    // public void SplitWordsTest()
    // {
    //     var split = "HoangBaoDuy".SplitWords();
    //     Assert.AreEqual(split.Length, 3);
    //     Assert.AreEqual(split[0], "Hoang");
    //     Assert.AreEqual(split[1], "Bao");
    //     Assert.AreEqual(split[2], "Duy");
    // }
}