using System;
using System.Linq;
using System.Linq.Expressions;
using Dandelion.Factory.Extensions;

namespace Expressions.Extensions
{
    public static class GenericExtensions
    {
        public static void To<TIn, TOut>(this TIn material, Expression<Func<TOut>> expression)
        {
            var body=(MethodCallExpression)expression.Body;
            var member=((MemberExpression)body.Arguments.First());
            var notifyChange = expression.Compile();

            material.To<TIn,TOut>(plant =>
                {
                    var binder=Expression.Assign(member,Expression.Constant(plant));
                    Expression.Lambda(binder).Compile().DynamicInvoke();
                    notifyChange();
                });
        } 
    }
}