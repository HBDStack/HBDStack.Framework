using System;

namespace HBDStack.Framework.Extensions;

public static class UserExtensions
{
    #region Methods

    /// <summary>
    /// Remove Domain name from User name follow these format Domain\username or username@domain.name
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    public static string WithoutDomain(this string userName)
    {
        if (string.IsNullOrWhiteSpace(userName)) return userName;

        var slashIndex = userName.IndexOf('\\', StringComparison.OrdinalIgnoreCase);
        if (slashIndex >= 0) return userName.Substring(slashIndex + 1);

        var atIndex = userName.IndexOf('@', StringComparison.OrdinalIgnoreCase);
        if (atIndex >= 0) return userName.Substring(0, atIndex);

        return userName;
    }

    #endregion Methods
}