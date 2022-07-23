using System;
using System.Linq;
using BenchmarkDotNet.Attributes;
using FluentAssertions;
using HBDStack.Framework.Extensions.Core;
using HBDStack.Framework.Extensions.Tests.TestObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HBDStack.Framework.Extensions.Tests;

[TestClass]
public class TestTypeExtractorExtensions
{
    #region Methods

    [TestMethod]
    [Benchmark]
    public void Test_Abstract()
    {
        typeof(TestEnum).Assembly.Extract().Abstract()
            .Should().HaveCountGreaterOrEqualTo(1);
    }

    [TestMethod]
    [Benchmark]
    public void Test_Duplicate_Assemblies()
    {
        new[] { typeof(ITypeExtractor).Assembly, typeof(ITypeExtractor).Assembly }
            .Extract().IsInstanceOf<ITypeExtractor>()
            .Count().Should().BeGreaterOrEqualTo(1);
    }

    [TestMethod]
    [Benchmark]
    public void Test_Interface()
    {
        typeof(TestEnum).Assembly.Extract().IsInstanceOf<ITem>()
            .Count().Should().BeGreaterOrEqualTo(1);
    }

    [TestMethod]
    [Benchmark]
    public void Test_Nested()
    {
        typeof(TestEnum).Assembly.Extract().Nested()
            .Should().HaveCountGreaterOrEqualTo(1);
    }

    [TestMethod]
    [Benchmark]
    public void Test_NotClass()
    {
        typeof(TestEnum).Assembly.Extract().NotClass()
            .Should().HaveCountGreaterOrEqualTo(1);
    }

    [TestMethod]
    [Benchmark]
    public void Test_NotEnum()
    {
        typeof(TestEnum).Assembly.Extract().NotEnum()
            .Should().HaveCountGreaterOrEqualTo(1);
    }

    [TestMethod]
    [Benchmark]
    public void TestExtract()
    {
        typeof(TestEnum).Assembly.Extract().Public().Class().Count()
            .Should().BeGreaterOrEqualTo(3);
    }

    [TestMethod]
    [Benchmark]
    public void TestExtract_GenericClass()
    {
        var list = typeof(TestEnum).Assembly.ScanGenericClassesWithFilter("Generic").ToList();
        list.Any().Should().BeTrue();
    }

    [TestMethod]
    [Benchmark]
    public void TestExtract_NotInstanceOf()
    {
        var list = typeof(TestEnum).Assembly.Extract().Class().NotInstanceOf(typeof(ITem)).ToList();
        list.Contains(typeof(TestItem)).Should().BeFalse();
        list.Contains(typeof(TestItem2)).Should().BeFalse();
    }
        
    [TestMethod]
    public void TestExtract_InstanceOfAny()
    {
        var list = typeof(TestEnum).Assembly.Extract().Class().IsInstanceOfAny(typeof(ITem),typeof(IConfigItem)).ToList();
        list.Count.Should().BeGreaterOrEqualTo(3);
    }

    [TestMethod]
    [Benchmark]
    public void TestScanClassesFromWithFilter()
    {
        typeof(TestEnum).Assembly.ScanClassesWithFilter("Item")
            .Count().Should().BeGreaterOrEqualTo(1);
    }

    [TestMethod]
    [Benchmark]
    public void TestScanClassesImplementOf()
    {
        typeof(TestEnum).Assembly.ScanClassesImplementOf(typeof(IDisposable))
            .Count().Should().BeGreaterOrEqualTo(1);
    }

    [TestMethod]
    [Benchmark]
    public void TestScanClassesImplementOf_Generic()
    {
        typeof(TestEnum).Assembly.ScanClassesImplementOf<GenericClassItem<TestItem>>()
            .Count().Should().BeGreaterOrEqualTo(1);
    }

    [TestMethod]
    [Benchmark]
    public void TestScanClassesImplementOf_GenericType()
    {
        typeof(Implemented).Assembly.ScanClassesImplementOf(typeof(GenericClassItem<>))
            .Count().Should().BeGreaterOrEqualTo(1);
    }

    [TestMethod]
    public void TestScanClassesImplementOf_GenericInterface()
    {
        var items = typeof(TestEnum).Assembly.ScanClassesImplementOf(typeof(IGenericClassItem<>));
               
        items .Count().Should().BeGreaterOrEqualTo(1);
        items.Any(c => c == typeof(Implemented)).Should().BeTrue();
    }

    [TestMethod]
    [Benchmark]
    public void TestScanPublicClassesFromWithFilter()
    {
        typeof(TestEnum).Assembly.ScanPublicClassesWithFilter("ConfigItem")
            .Count().Should().BeGreaterOrEqualTo(1);
    }

    [TestMethod]
    [Benchmark]
    public void TestScanPublicClassesImplementOf()
    {
        typeof(TestEnum).Assembly.ScanPublicClassesImplementOf<IConfigItem>()
            .Count().Should().BeGreaterOrEqualTo(1);
    }

    #endregion Methods
}