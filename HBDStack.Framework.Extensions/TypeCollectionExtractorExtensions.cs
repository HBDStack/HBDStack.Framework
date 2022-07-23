using System;
using System.Collections.Generic;
using System.Reflection;
using HBDStack.Framework.Extensions.Core;
using HBDStack.Framework.Extensions.Internal;

namespace HBDStack.Framework.Extensions;

public static class TypeCollectionExtractorExtensions
{
    #region Methods

    public static ITypeExtractor Extract(this ICollection<Assembly> assemblies)
        => new TypeExtractor(assemblies);

    /// <summary>
    /// Get Public and Private classes which implement of T
    /// </summary>
    /// <param name="assemblies"></param>
    /// <returns></returns>
    public static IEnumerable<Type> ScanClassesImplementOf<T>(this ICollection<Assembly> assemblies)
        => assemblies.ScanClassesImplementOf(typeof(T));

    /// <summary>
    /// Get Public and Private classes which implement of T
    /// </summary>
    /// <param name="assemblies"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public static IEnumerable<Type> ScanClassesImplementOf(this ICollection<Assembly> assemblies, Type type)
        => new TypeExtractor(assemblies).Class().NotAbstract().NotGeneric()
            .IsInstanceOf(type);

    /// <summary>
    /// Get Public and Private classes which name contains the nameContains
    /// </summary>
    /// <param name="assemblies"></param>
    /// <param name="nameContains"></param>
    /// <returns></returns>
    public static IEnumerable<Type> ScanClassesWithFilter(this ICollection<Assembly> assemblies, string nameContains)
        => assemblies.Extract().Class().NotAbstract().NotGeneric()
            .Where(t => t.Name.Contains(nameContains, StringComparison.OrdinalIgnoreCase));

    /// <summary>
    /// Get Public classes which name contains the nameContains
    /// </summary>
    /// <param name="assemblies"></param>
    /// <param name="nameContains"></param>
    /// <returns></returns>
    public static IEnumerable<Type> ScanGenericClassesWithFilter(this ICollection<Assembly> assemblies, string nameContains)
        => assemblies.Extract().Generic().Class()
            .Where(t => t.Name.Contains(nameContains, StringComparison.OrdinalIgnoreCase));

    /// <summary>
    /// Get Public classes which implement of T
    /// </summary>
    /// <param name="assemblies"></param>
    /// <returns></returns>
    public static IEnumerable<Type> ScanPublicClassesImplementOf<T>(this ICollection<Assembly> assemblies)
        => assemblies.ScanPublicClassesImplementOf(typeof(T));

    /// <summary>
    /// Get Public classes which implement of type
    /// </summary>
    /// <param name="assemblies"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public static IEnumerable<Type> ScanPublicClassesImplementOf(this ICollection<Assembly> assemblies, Type type)
        => assemblies.Extract().Public().Class().NotAbstract().NotGeneric()
            .IsInstanceOf(type);

    /// <summary>
    /// Get Public classes which name contains the nameContains
    /// </summary>
    /// <param name="assemblies"></param>
    /// <param name="nameContains"></param>
    /// <returns></returns>
    public static IEnumerable<Type> ScanPublicClassesWithFilter(this ICollection<Assembly> assemblies, string nameContains)
        => assemblies.Extract().Public().Class().NotAbstract().NotGeneric()
            .Where(t => t.Name.Contains(nameContains, StringComparison.OrdinalIgnoreCase));

    #endregion Methods
}