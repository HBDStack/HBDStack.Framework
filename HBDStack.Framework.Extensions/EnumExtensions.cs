using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

// ReSharper disable MemberCanBePrivate.Global

namespace HBDStack.Framework.Extensions;

public static class EnumExtensions
{

    public static T GetAttribute<T>(this Enum @this) where T : Attribute
    {
        if (@this == null) return null;

        var type = @this.GetType();
        var f = type.GetField(@this.ToString());
        return f?.GetCustomAttribute<T>();
    }

    public static EnumInfo GetEumInfo(this Enum @this)
    {
        if (@this == null) return null;
        
        var att = @this.GetAttribute<DisplayAttribute>();

        return new EnumInfo
        {
            Key = @this.ToString(),
            Description = att?.Description,
            Name = att?.Name,
            GroupName = att?.GroupName
        };
    }

    public static IEnumerable<EnumInfo> GetEumInfos<T>() where T : Enum
    {
        var type = typeof(T);
        var members = type.GetFields();

        foreach (var info in members)
        {
            if (info.FieldType == typeof(int)) continue;

            var att = info.GetCustomAttribute<DisplayAttribute>();

            yield return new EnumInfo
            {
                Key = info.Name,
                Description = att?.Description,
                Name = att?.Name ?? info.Name,
                GroupName = att?.GroupName
            };
        }
    }
    
}