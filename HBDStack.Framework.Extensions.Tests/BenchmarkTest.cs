using BenchmarkDotNet.Running;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HBDStack.Framework.Extensions.Tests;

[TestClass]
public class BenchmarkTest
{
    #region Methods

    [TestMethod]
    public void Test_TypeExtractor()
    {
        var summary = BenchmarkRunner.Run<TestTypeExtractorExtensions>();
    }

    #endregion Methods
}