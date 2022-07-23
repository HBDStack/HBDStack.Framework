using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
// ReSharper disable CA1710
// ReSharper disable CA1716

namespace HBDStack.Framework.Extensions.Core;

[SuppressMessage("Naming", "CA1716:Identifiers should not match keywords")]
public interface ITypeExtractor : IEnumerable<Type>
{
    #region Methods

    ITypeExtractor Abstract();

    ITypeExtractor Class();

    ITypeExtractor Enum();

    ITypeExtractor Generic();

    ITypeExtractor HasAttribute<TAttribute>() where TAttribute : Attribute;

    ITypeExtractor HasAttribute(Type attributeType);

    ITypeExtractor Interface();

    ITypeExtractor IsInstanceOf(Type type);

    ITypeExtractor IsInstanceOf<T>();
        
    ITypeExtractor IsInstanceOfAny(params Type[] type);

    ITypeExtractor Nested();

    ITypeExtractor NotAbstract();

    ITypeExtractor NotClass();

    ITypeExtractor NotEnum();

    ITypeExtractor NotGeneric();

    ITypeExtractor NotInstanceOf(Type type);

    ITypeExtractor NotInstanceOf<T>();

    ITypeExtractor NotInterface();

    ITypeExtractor NotNested();

    ITypeExtractor NotPublic();

    ITypeExtractor Public();

    ITypeExtractor Where(Expression<Func<Type, bool>> predicate);

    #endregion Methods
}