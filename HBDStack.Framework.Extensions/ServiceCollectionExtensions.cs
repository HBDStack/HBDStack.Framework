using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection Add<TService, TImplementation>([NotNull] this IServiceCollection services,
        ServiceLifetime lifetime = ServiceLifetime.Scoped) where TImplementation : class, TService
    {
        services.Add(new ServiceDescriptor(typeof(TService), typeof(TImplementation), lifetime));
        return services;
    }

    public static IServiceCollection RemoveAll<TService>([NotNull] this IServiceCollection services)
    {
        foreach (var found in services.Where(s => s.ServiceType == typeof(TService)))
            services.Remove(found);

        return services;
    }

    public static IServiceCollection Remove<TImplementation>([NotNull] this IServiceCollection services)
    {
        var found = services.FirstOrDefault(s =>
            s.ImplementationType == typeof(TImplementation) || s.ImplementationInstance is TImplementation)
            ?? services.FirstOrDefault(s=>s.ServiceType == typeof(TImplementation));
        
        if (found != null)
            services.Remove(found);
        return services;
    }

    public static IServiceCollection Replace<TService, TImplementation>([NotNull] this IServiceCollection services,
        ServiceLifetime lifetime = ServiceLifetime.Scoped) where TImplementation : class, TService =>
        services.RemoveAll<TService>().Add<TService, TImplementation>(lifetime);

    public static IServiceCollection AsImplementedInterfaces(this IServiceCollection services,
        IEnumerable<Type> types, ServiceLifetime lifetime = ServiceLifetime.Scoped)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));
        if (types == null) throw new ArgumentNullException(nameof(types));

        foreach (var classType in types)
        {
            if (classType.IsInterface) continue;

            var interfaces = classType.GetInterfaces()
                .Where(i => i != typeof(IDisposable) && i.IsPublic);

            //Add Interfaces
            foreach (var i in interfaces)
                services.Add(new ServiceDescriptor(i, classType, lifetime));

            //Add itself
            services.Add(new ServiceDescriptor(classType, classType, lifetime));
        }

        return services;
    }

    public static bool Contains<TImplement>(this IServiceCollection services, ServiceLifetime serviceLifetime)
        => services.Contains(typeof(TImplement), serviceLifetime);

    public static bool Contains(this IServiceCollection services, Type implementType, ServiceLifetime serviceLifetime)
        => services.Any(s => s.ImplementationType == implementType && s.Lifetime == serviceLifetime);

    public static bool Contains<TServiceType, TImplement>(this IServiceCollection services,
        ServiceLifetime serviceLifetime)
        => services.Contains(typeof(TServiceType), typeof(TImplement), serviceLifetime);

    public static bool Contains(this IServiceCollection services, Type serviceType, Type implementType,
        ServiceLifetime serviceLifetime)
        => services.Any(s =>
            s.ServiceType == serviceType && s.ImplementationType == implementType && s.Lifetime == serviceLifetime);

    /// <summary>
    /// Create Instance with parameters from IServiceProvider
    /// </summary>
    /// <param name="provider"></param>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>
    public static TService CreateInstance<TService>(this IServiceProvider provider) where TService : class
    {
        var type = typeof(TService);
        var types = type.GetConstructors().First(f => f.IsPublic).GetParameters().Select(p => p.ParameterType);
        var objs = types.Select(t => t == typeof(IServiceProvider) ? provider : provider.GetRequiredService(t))
            .ToArray();

        return Activator.CreateInstance(type, objs) as TService;
    }
}