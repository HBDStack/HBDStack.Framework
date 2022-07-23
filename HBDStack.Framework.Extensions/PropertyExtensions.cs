using HBDStack.Framework.Extensions.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace HBDStack.Framework.Extensions;

public static class PropertyExtensions
{
    // public static IEnumerable<PropertyAttributeInfo<TAttribute>> GetProperties<TAttribute>(this object @this,
    //     bool inherit = true) where TAttribute : Attribute
    // {
    //     if (@this == null) yield break;
    //     foreach (var p in @this.GetType().GetProperties())
    //     {
    //         var att = p.GetCustomAttribute<TAttribute>(inherit);
    //         if (att == null) continue;
    //         yield return new PropertyAttributeInfo<TAttribute> { Attribute = att, PropertyInfo = p };
    //     }
    // }

    public static PropertyInfo GetProperty<T>(this T @this, string propertyName) where T : class
    {
        if (@this == null || string.IsNullOrEmpty(propertyName)) return null;
        var type = @this is Type t ? t : @this.GetType();

        return type
            .GetProperty(propertyName,
                BindingFlags.IgnoreCase
                | BindingFlags.Public
                | BindingFlags.NonPublic
                | BindingFlags.Instance);
    }

    public static object PropertyValue<T>(this T obj, string propertyName) where T : class
    {
        if (obj == null) return null;
        if (string.IsNullOrEmpty(propertyName)) return null;

        var props = propertyName.Contains(".", StringComparison.OrdinalIgnoreCase)
            ? propertyName.Split('.')
            : new[] { propertyName };

        var currentObj =
            props.Aggregate<string, object>(obj, (current, p) => current.GetProperty(p)?.GetValue(current));
        return currentObj == obj ? null : currentObj;
    }

    public static void SetPropertyValue(this object @this, PropertyInfo property, object value)
    {
        if (@this == null) throw new ArgumentNullException(nameof(@this));
        if (property == null) throw new ArgumentNullException(nameof(property));

        if (value == null)
            property.SetValue(@this, null, null);
        else
        {
            value = property.PropertyType.IsEnum
                ? Enum.Parse(property.PropertyType, value.ToString()!)
                : Convert.ChangeType(value, property.PropertyType, CultureInfo.CurrentCulture);

            property.SetValue(@this, value, null);
        }
    }

    public static void SetPropertyValue(this object @this, string propertyName, object value)
    {
        if (@this == null) throw new ArgumentNullException(nameof(@this));
        if (string.IsNullOrEmpty(propertyName)) throw new ArgumentNullException(nameof(propertyName));

        var property = @this.GetProperty(propertyName);
        @this.SetPropertyValue(property, value);
    }
}