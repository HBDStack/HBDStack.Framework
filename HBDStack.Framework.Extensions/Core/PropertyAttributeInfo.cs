using System;
using System.Reflection;

namespace HBDStack.Framework.Extensions.Core;

public class PropertyAttributeInfo<TAttribute> where TAttribute : Attribute
{
    #region Properties

    public TAttribute Attribute { get; protected internal set; }

    public PropertyInfo PropertyInfo { get; protected internal set; }

    #endregion Properties
}