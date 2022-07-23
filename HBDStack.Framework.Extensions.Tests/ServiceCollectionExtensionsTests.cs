using System;
using FluentAssertions;
using HBDStack.Framework.Extensions.Tests.TestObjects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HBDStack.Framework.Extensions.Tests;

[TestClass]
public class ServiceCollectionExtensionsTests
{
    private ServiceProvider _service;
    
    [TestInitialize]
   public void Setup()
   {
        _service = new ServiceCollection().AddLogging().AddSingleton<ITestInterface, TestClass>()
           .BuildServiceProvider();
   }
    
    [TestCleanup]
    public void Cleanup() => _service?.Dispose();

    [TestMethod]
    public void Test()
    {
        var item = _service.CreateInstance<TestServiceActivator>();
        item.Should().NotBeNull();
    }
    
    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void TestGetDirect() => _service.GetRequiredService<TestServiceActivator>();
}