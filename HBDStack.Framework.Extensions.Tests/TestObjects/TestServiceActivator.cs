using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace HBDStack.Framework.Extensions.Tests.TestObjects;

public interface ITestInterface
{
}

public class TestClass : ITestInterface
{
}

public class TestServiceActivator
{
    private readonly IServiceProvider _provider;
    private readonly ILogger<TestServiceActivator> _logger;
    private readonly ITestInterface _interface;

    public TestServiceActivator(IServiceProvider provider, ILogger<TestServiceActivator> logger,
        IEnumerable<ITestInterface> interfaces)
    {
        _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _interface = interfaces.FirstOrDefault()?? throw new ArgumentNullException(nameof(interfaces));
    }
}