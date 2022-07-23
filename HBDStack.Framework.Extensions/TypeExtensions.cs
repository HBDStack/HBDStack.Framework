using System;
using System.Linq;

namespace HBDStack.Framework.Extensions;

public static class TypeExtensions
{
    // public static T ChangeType<T>(this object @this)
    // {
    //     if (@this is null)
    //     {
    //         throw new ArgumentNullException(nameof(@this));
    //     }
    //
    //     if (typeof(T) == typeof(object)) return (T)@this;
    //     if (@this.IsNull()) return default(T);
    //     if (typeof(T) == typeof(bool) && !(@this is bool))
    //     {
    //         var str = @this.ToString();
    //         @this = str.Equals("1", StringComparison.OrdinalIgnoreCase) || string.Compare(str, "Yes", StringComparison.CurrentCultureIgnoreCase) == 0;
    //     }
    //
    //     var value = Convert.ChangeType(@this, typeof(T));
    //
    //     if (value == null) return default(T);
    //     return (T)value;
    // }

    // public static object ChangeType(this object @this, Type newType)
    // {
    //     if (@this == null) return null;
    //     if (@this.GetType() == newType) return @this;
    //
    //     try
    //     {
    //         return Convert.ChangeType(@this, newType);
    //     }
    //     catch
    //     {
    //         return null;
    //     }
    // }

    /// <summary>
    /// Convert string to Type T. Support boolean from true/false or 1/0
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    // public static T ConvertTo<T>(this string @this)
    // {
    //     if (@this.IsNullOrEmpty()) return default(T);
    //
    //     var type = typeof(T);
    //
    //     object obj = null;
    //
    //     if (type == @this.GetType())
    //         obj = @this;
    //     else
    //         obj = type == typeof(bool)
    //             ? bool.TrueString.EqualsIgnoreCase(@this) || @this == "1"
    //             : Convert.ChangeType(@this, type);
    //     return (T)obj;
    // }

    //public static bool IsAssignableFrom<T>(this Type @this)
    //{
    //    return @this != null && @this.IsAssignableFrom(typeof(T));
    //}

    //public static bool IsNotAssignableFrom(this Type @this, Type type)
    //{
    //    if (@this == null || type == null) return false;
    //    return !@this.IsAssignableFrom(type);
    //}

    //public static bool IsNotAssignableFrom<T>(this Type @this)
    //{
    //    return !@this.IsAssignableFrom<T>();
    //}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="matching"></param>
    /// <returns></returns>
    public static bool IsImplementOf(this Type type, Type matching)
    {
        if (type == null || matching == null) return false;

        if (type == matching) return false;

        if (matching.IsAssignableFrom(type)) return true;

        if (matching.IsInterface)
            return type.GetInterfaces().Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == matching || matching.IsAssignableFrom(y));

        while (type != null)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == matching)
                return true;
            type = type.BaseType;
        }
        return false;
    }

    public static bool IsImplementOf<T>(this Type type)
        => type.IsImplementOf(typeof(T));

    public static bool IsNotNumericType(this object @this) => !@this.IsNumericType();

    public static bool IsNumericType(this Type @this)
    {
        if (@this == null) return false;

        switch (Type.GetTypeCode(@this))
        {
            case TypeCode.Byte:
            case TypeCode.SByte:
            case TypeCode.UInt16:
            case TypeCode.UInt32:
            case TypeCode.UInt64:
            case TypeCode.Int16:
            case TypeCode.Int32:
            case TypeCode.Int64:
            case TypeCode.Decimal:
            case TypeCode.Double:
            case TypeCode.Single:
                return true;

            case TypeCode.Empty:
            case TypeCode.Object:
            case TypeCode.DBNull:
            case TypeCode.Boolean:
            case TypeCode.Char:
            case TypeCode.DateTime:
            case TypeCode.String:
            default:
                return false;
        }
    }

    public static bool IsNumericType(this object @this) => @this?.GetType().IsNumericType() ?? false;
}