using FluentAssertions;
using HBDStack.Framework.Extensions.Tests.TestObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HBDStack.Framework.Extensions.Tests;

[TestClass]
public class EnumExtensionsTests
{
    #region Methods

    [TestMethod]
    public void GetAttribute()
    {
        HBDEnum.DescriptionEnum.GetAttribute<DisplayAttribute>()
            .Should().NotBeNull();
    }

    [TestMethod]
    public void TestGetEnumInfo()
    {
        HBDEnum.DescriptionEnum.GetEumInfo().Name.Should().Be("HBD");
    }

    [TestMethod]
    public void TestGetEnumInfos()
    {
        var list = EnumExtensions.GetEumInfos<HBDEnum>().ToList();
        list.Count.Should().Be(3);
    }

    #endregion Methods
}