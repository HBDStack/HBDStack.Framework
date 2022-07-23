using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HBDStack.Framework.Extensions.Tests;

[TestClass]
public class RunnerTests
{
    [TestMethod]
    public void RunAction()
    {
        var count = 0;
        Action a = () =>
        {
            Console.WriteLine("Action A");
            count += 1;
        };
        Action b = () =>
        {
            Console.WriteLine("Action B");
            count += 1;
        };

        Runner.Once(a);
        Runner.Once(a);
        count.Should().Be(1);

        Runner.Once(b);
        Runner.Once(b);

        count.Should().Be(2);
    }

    [TestMethod]
    public async Task RunActionAsync()
    {
        var count = 0;
        var a = () =>
        {
            Console.WriteLine("Action A");
            count += 1;
            return Task.CompletedTask;
        };
        var b = () =>
        {
            Console.WriteLine("Action B");
            count += 1;
            return Task.CompletedTask;
        };

        await Runner.Once(a);
        await Runner.Once(a);
        count.Should().Be(1);

        await Runner.Once(b);
        await Runner.Once(b);

        count.Should().Be(2);
    }
}