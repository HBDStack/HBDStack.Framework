﻿using HBDStack.Framework.Extensions.Tests.TestObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;

namespace HBDStack.Framework.Extensions.Tests;

[TestClass]
public class AttributeExtensionsTestCases
{
    #region Methods

    // [TestMethod]
    // [TestCategory("Fw.Extensions")]
    // public void GetAttribute()
    // {
    //     Assert.IsTrue(this.GetAttribute<TestClassAttribute>() != null);
    // }

    [TestMethod]
    [TestCategory("Fw.Extensions")]
    [ExpectedException(typeof(ArgumentNullException))]
    public void GetAttribute_WithNullType_ReturnNull_Test()
    {
        Type type = null;
        Assert.IsNull(type.GetCustomAttribute(typeof(TestAttribute)));
        Assert.IsNull(((PropertyInfo)null).GetCustomAttribute<TestAttribute>());
    }

    [TestMethod]
    [TestCategory("Fw.Extensions")]
    public void GetAttribute_WithType_ReturnExpectedAttribute_Test()
    {
        var type = typeof(HasAttributeTestClass1);
        Assert.IsNotNull(type.GetCustomAttribute(typeof(TestAttribute)));
        Assert.IsNotNull(type.GetCustomAttribute(typeof(TestAttribute)) is TestAttribute);
    }

    [TestMethod]
    [TestCategory("Fw.Extensions")]
    public void GetAttributeGeneric_WithType_ReturnExpectedAttribute_Test()
    {
        var type = typeof(HasAttributeTestClass1);
        Assert.IsNotNull(type.GetCustomAttribute<TestAttribute>());
    }

    [TestMethod]
    [TestCategory("Fw.Extensions")]
    public void HasAttribute_Test()
    {
        var obj1 = new HasAttributeTestClass1();
        Assert.IsTrue(obj1.GetType().HasAttribute<TestAttribute>());
        Assert.IsTrue(obj1.HasAttributeOnProperty<TestAttribute>("Prop1"));

        var obj2 = new HasAttributeTestClass2();
        Assert.IsTrue(obj2.GetType().HasAttribute<TestAttribute>());
        Assert.IsTrue(obj2.HasAttributeOnProperty<TestAttribute>("Prop1"));

        var obj3 = new HasAttributeTestClass3();
        Assert.IsFalse(obj3.GetType().HasAttribute<TestAttribute>());
        Assert.IsFalse(obj3.HasAttributeOnProperty<TestAttribute>("Prop3"));
    }

    [TestMethod]
    [TestCategory("Fw.Extensions")]
    public void NullPropertyInfo_HasAttribute_ReturnsFalse_Test()
    {
        Assert.IsFalse(((PropertyInfo)null).HasAttribute<TestAttribute>());
    }

    #endregion Methods
}