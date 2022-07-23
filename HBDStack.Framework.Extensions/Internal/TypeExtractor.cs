using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using HBDStack.Framework.Extensions.Core;

namespace HBDStack.Framework.Extensions.Internal;

internal class TypeExtractor : ITypeExtractor
{
    #region Fields

    private IQueryable<Type> _query;

    #endregion Fields

    #region Constructors

    public TypeExtractor(params Assembly[] assemblies)
    {
        if (assemblies == null || assemblies.Length <= 0)
            throw new ArgumentNullException(nameof(assemblies));

        _query = assemblies.SelectMany(a => a.GetTypes()).AsQueryable();
    }

    public TypeExtractor(ICollection<Assembly> assemblies)
    {
        if (assemblies == null || assemblies.Count <= 0)
            throw new ArgumentNullException(nameof(assemblies));

        _query = assemblies.SelectMany(a => a.GetTypes()).AsQueryable();
    }

    #endregion Constructors

    #region Methods

    public ITypeExtractor Abstract() => Where(t => t.IsAbstract);

    public ITypeExtractor Class() => Where(t => t.IsClass);

    public ITypeExtractor Enum() => Where(t => t.IsEnum);

    public ITypeExtractor Generic() => Where(t => t.IsGenericType);

    public IEnumerator<Type> GetEnumerator() => _query.Distinct().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public ITypeExtractor HasAttribute<TAttribute>() where TAttribute : Attribute
        => HasAttribute(typeof(TAttribute));

    public ITypeExtractor HasAttribute(Type attributeType)
        => Where(t => t.GetCustomAttribute(attributeType) != null);

    public ITypeExtractor Interface() => Where(t => t.IsInterface);


    public ITypeExtractor IsInstanceOf(Type type)
        => Where(t => t.IsImplementOf(type));

    public ITypeExtractor IsInstanceOf<T>() => IsInstanceOf(typeof(T));

    public ITypeExtractor IsInstanceOfAny(params Type[] types)
        => Where(t => types.Any(t.IsImplementOf));
        
    public ITypeExtractor Nested() => Where(t => t.IsNested);

    public ITypeExtractor NotAbstract() => Where(t => !t.IsAbstract);

    public ITypeExtractor NotClass() => Where(t => !t.IsClass);

    public ITypeExtractor NotEnum() => Where(t => !t.IsEnum);

    public ITypeExtractor NotGeneric() => Where(t => !t.IsGenericType);

    public ITypeExtractor NotInstanceOf(Type type)
        => Where(t => !t.IsImplementOf(type));

    public ITypeExtractor NotInstanceOf<T>() => NotInstanceOf(typeof(T));

    public ITypeExtractor NotInterface() => Where(t => !t.IsInterface);

    public ITypeExtractor NotNested() => Where(t => !t.IsNested);

    public ITypeExtractor NotPublic() => Where(t => !t.IsPublic);

    public ITypeExtractor Public() => Where(t => t.IsPublic);

    public ITypeExtractor Where(Expression<Func<Type, bool>> predicate)
    {
        if (predicate != null)
            _query = _query.Where(predicate);
        return this;
    }

    #endregion Methods
}