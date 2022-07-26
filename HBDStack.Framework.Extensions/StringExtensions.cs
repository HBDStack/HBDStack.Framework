﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

// ReSharper disable MemberCanBePrivate.Global

namespace HBDStack.Framework.Extensions;

public static partial class StringExtensions
{
    // private static readonly Regex Reg = new("([a-z](?=[0-9])|[a-z,0-9](?=[A-Z])|[A-Z](?=[A-Z][a-z]))", RegexOptions.Compiled);

    /// <summary>
    /// Check whether the string is contains all items in the list of values. Ignore Case when do
    /// the comparison.
    /// </summary>
    /// <param name="this"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    // public static bool AllIgnoreCase(this string @this, params string[] values)
    //     => values.All(@this.ContainsIgnoreCase);

    /// <summary>
    /// Check whether the string collection is contains item. Ignore Case when do the comparison.
    /// </summary>
    /// <param name="this"></param>
    /// <param name="item"></param>
    /// <returns></returns>
    // public static bool AnyIgnoreCase(this IEnumerable<string> @this, string item)
    //     => @this?.Any(s => s.EqualsIgnoreCase(item)) == true;

    /// <summary>
    /// Check whether the string is contains any item in the list of values.
    /// </summary>
    /// <param name="this"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    // public static bool AnyOf(this string @this, params string[] values)
    // {
    //     if (@this == null) throw new ArgumentNullException(nameof(@this));
    //     return values.Any(@this.Contains);
    // }

    /// <summary>
    /// Check whether the string is contains any item in the list of values. Ignore Case when do
    /// the comparison.
    /// </summary>
    /// <param name="this"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    // public static bool AnyOfIgnoreCase(this string @this, params string[] values)
    //     => values.Any(@this.ContainsIgnoreCase);
    //
    // public static bool ContainsIgnoreCase(this string @this, string value)
    //     => @this?.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0;

    /// <summary>
    /// Count the occurrences of substring in a string.
    /// </summary>
    /// <param name="this"></param>
    /// <param name="value"></param>
    /// <param name="comparison"></param>
    /// <returns></returns>
    // public static int Count(this string @this, string value, StringComparison comparison = StringComparison.Ordinal)
    // {
    //     if (@this.IsNullOrEmpty() || value.IsNullOrEmpty()) return 0;
    //
    //     var lastIndex = 0;
    //     var count = 0;
    //
    //     while (lastIndex != -1)
    //     {
    //         lastIndex = @this.IndexOf(value, lastIndex, comparison);
    //
    //         if (lastIndex == -1) continue;
    //         count++;
    //         lastIndex += value.Length;
    //     }
    //
    //     return count;
    // }

    /// <summary>
    /// Count the occurrences of substring in a string with OrdinalIgnoreCase
    /// </summary>
    /// <param name="this"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    // public static int CountIgnoreCase(this string @this, string value)
    //     => @this.Count(value, StringComparison.OrdinalIgnoreCase);
    //
    // public static bool EqualsIgnoreCase(this string @this, string value)
    //     => @this?.Equals(value, StringComparison.OrdinalIgnoreCase) == true;

    public static string ExtractNumber(this string @this) =>
        new(@this.Where(c => char.IsDigit(c) || c is '.' or ',' or '-').ToArray());

    // public static bool IsEmail(this string email)
    // {
    //     var regex = new Regex(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$",
    //         RegexOptions.Compiled);
    //     return regex.IsMatch(email);
    // }
    //
    // public static bool IsNotNullOrEmpty(this string @this) => !string.IsNullOrWhiteSpace(@this);
    //
    // public static bool IsNotNumber(this string @this) => !@this.IsNumber();
    //
    // public static bool IsNullOrEmpty(this string @this) => string.IsNullOrWhiteSpace(@this);

    public static bool IsNumber(this string @this)
    {
        if (string.IsNullOrWhiteSpace(@this)) return false;
        if (@this.Count(c => c == '.') > 1) return false;
        if (@this.Contains(",,", StringComparison.OrdinalIgnoreCase)) return false;
        if (@this.LastIndexOf("-", StringComparison.Ordinal) > 0) return false;
        return @this.Where(c => c != '.' && c != ',' && c != '-').All(c => c is >= '0' and <= '9');
    }

    // public static bool IsStringOrValueType(this object @this)
    // {
    //     if (@this == null) return false;
    //     return (@this as Type)?.IsStringOrValueType()
    //            ?? (@this as PropertyInfo)?.IsStringOrValueType()
    //            ?? @this.GetType().IsStringOrValueType();
    // }

    public static bool IsStringOrValueType(this PropertyInfo @this)
        => @this?.PropertyType.IsStringOrValueType() == true;

    public static bool IsStringOrValueType(this Type @this)
    {
        if (@this == null) return false;

        if (@this.IsGenericType)
        {
            //The Nullable type.
            @this = @this.GenericTypeArguments[0];
        }

        var result = Type.GetTypeCode(@this) == TypeCode.String || !@this.IsGenericType
            && @this.IsValueType;

        //Null able is IsValueType and IsGenericType
        return result;
    }

    // public static bool NotEqualsIgnoreCase(this string @this, string value)
    //     => !@this.EqualsIgnoreCase(value);
    //
    // public static string Remove(this string @this, params string[] values)
    // {
    //     if (@this.IsNullOrEmpty() || values?.NotAny() == true) return @this;
    //
    //     // ReSharper disable once AssignNullToNotNullAttribute
    //     return values!.Where(va => !va.IsNullOrEmpty())
    //         .Aggregate(@this, (current, va) => current.Replace(va, string.Empty, StringComparison.OrdinalIgnoreCase));
    // }

    /// <summary>
    /// Replace the string Ignore the sensitive Case.
    /// </summary>
    /// <param name="this"></param>
    /// <param name="oldValue"></param>
    /// <param name="newValue"></param>
    /// <returns></returns>
    // public static string ReplaceIgnoreCase(this string @this, string oldValue, string newValue)
    // {
    //     if (@this.IsNullOrEmpty() || newValue.IsNullOrEmpty()) return @this;
    //
    //     return Regex.Replace(
    //         @this,
    //         Regex.Escape(oldValue),
    //         newValue.Replace("$", "$$", StringComparison.OrdinalIgnoreCase),
    //         RegexOptions.IgnoreCase);
    // }
    //
    // public static string[] SplitWords(this string @this)
    //     => @this.ToDisplayWords().Split(new[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
    //
    // public static bool StartsWithIgnoreCase(this string @this, string value)
    //     => @this?.StartsWith(value, StringComparison.OrdinalIgnoreCase) == true;

    /// <summary>
    /// Insert the space to between 2 words.
    /// Ex: input: HelloWorld output: Hello World.
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    // public static string ToDisplayWords(this string @this)
    // {
    //     //Func<char, bool> isLowChar = (c) => c >= 'a' && c <= 'z';
    //     //bool IsUpChar(char c) => c >= 'A' && c <= 'Z';
    //     //bool IsNumChar(char c) => c >= '0' && c <= '9';
    //     //bool IsChange(char c, char l) => IsUpChar(c) && (!IsUpChar(l) || IsNumChar(l)) || IsNumChar(c) && !IsNumChar(l);
    //
    //     //if (@this.IsNullOrEmpty()) return @this;
    //
    //     //var builder = new StringBuilder();
    //     //var lastCharater = char.MinValue;
    //
    //     //for (var i = 0; i < @this.Length; i++)
    //     //{
    //     //    var c = @this[i];
    //     //    if (lastCharater == char.MinValue) lastCharater = c;
    //
    //     // if (IsChange(c, lastCharater) && i - 1 > 0 && @this[i - 1] != ' ') builder.Append(' ');
    //
    //     //    builder.Append(c);
    //     //    lastCharater = c;
    //     //}
    //
    //     //return builder.ToString();
    //     return Reg.Replace(@this, "$1 ");
    // }
}