﻿using System;
using System.Reflection;

// ReSharper disable MemberCanBePrivate.Global

namespace HBDStack.Framework.Extensions;

public static class AttributeExtensions
{
    public static bool HasAttribute<TAttribute>(this PropertyInfo @this, bool inherit = true)
        where TAttribute : Attribute
        => @this?.GetCustomAttribute<TAttribute>(inherit) != null;

    public static bool HasAttribute<TAttribute>(this Type @this, bool inherit = true)
        where TAttribute : Attribute
        => @this?.GetCustomAttribute<TAttribute>(inherit) != null;

    public static bool HasAttributeOnProperty<TAttribute>(this object @this, string propertyName,
        bool inherit = true) where TAttribute : Attribute
    {
        var prop = @this.GetProperty(propertyName);
        return prop.HasAttribute<TAttribute>(inherit);
    }
}