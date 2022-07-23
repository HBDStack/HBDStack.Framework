using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace HBDStack.Framework.Extensions;

public static class ExpressionExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="this"></param>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <returns></returns>
    public static IEnumerable<PropertyInfo> ExtractProperties<T, TKey>(this Expression<Func<T, TKey>> @this)
        where T : class
    {
        if (@this == null) yield break;

        var queue = new Queue<Expression>();
        queue.Enqueue(@this.Body);

        while (queue.Count > 0)
        {
            var ex = queue.Dequeue();

            switch (ex)
            {
                case MemberExpression expression:
                {
                    dynamic tmp = expression;
                    yield return tmp.Member;
                    break;
                }
                case UnaryExpression expression:
                {
                    dynamic tmp = expression.Operand as MemberExpression;
                    yield return tmp?.Member;
                    break;
                }
                case BinaryExpression expression:
                {
                    var tmp = expression;
                    queue.Enqueue(tmp.Left);
                    queue.Enqueue(tmp.Right);
                    break;
                }
                case MethodCallExpression expression:
                {
                    dynamic tmp = expression;
                    yield return tmp.Object.Member;
                    break;
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="this"></param>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <returns></returns>
    public static PropertyInfo ExtractProperty<T, TKey>(this Expression<Func<T, TKey>> @this)
        where T : class =>
        @this.ExtractProperties().SingleOrDefault();
}