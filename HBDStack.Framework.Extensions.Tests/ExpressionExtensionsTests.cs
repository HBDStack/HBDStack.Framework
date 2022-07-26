﻿using System;
using System.Linq;
using System.Linq.Expressions;
using HBDStack.Framework.Extensions.Tests.TestObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HBDStack.Framework.Extensions.Tests;

[TestClass]
public class ExpressionExtensionsTests
{
    #region Methods

    [TestMethod]
    [TestCategory("Fw.Extensions")]
    public void ExtractProperties_Test()
    {
        Expression<Func<TestItem, bool>> ex = i => i.Name != null && i.Id > 0;
        var properties = ex.ExtractProperties().ToArray();
        Assert.IsTrue(properties.Length == 2);
        Assert.IsTrue(properties[0].Name == "Name");
        Assert.IsTrue(properties[1].Name == "Id");
    }

    [TestMethod]
    [TestCategory("Fw.Extensions")]
    public void GetProperty_NullExpress_Test()
    {
        Expression<Func<TestItem, object>> ex = null;
        Assert.IsFalse(ex.ExtractProperties().Any());
    }

    [TestMethod]
    [TestCategory("Fw.Extensions")]
    public void GetProperty_Test()
    {
        Expression<Func<TestItem, object>> ex = i => i.Name;
        Assert.IsTrue(ex.ExtractProperties().First().Name == "Name");
    }

    #endregion Methods
}