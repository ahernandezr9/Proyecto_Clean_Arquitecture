using System;
using System.Linq.Expressions;

namespace Clean_Arquitecture.Entities.Specifications
{
    public class Specification<T>
    {
        //public abstract Expression<Func<T, bool>> Expression { get; }
        //public bool ISSatisfiedBy(T entity)
        //{
        //    Func<T, bool> ExpressionDelegate = Expression.Compile();
        //    return ExpressionDelegate(entity);
        //}
        public Expression<Func<T, bool>> Expression { get; private set; }
        public Specification(Expression<Func<T, bool>> expression)
        {
            Expression = expression;
        }
        public bool ISSatisfiedBy(T entity)
        {
            Func<T, bool> ExpressionDelegate = Expression.Compile();
            return ExpressionDelegate(entity);
        }
    }
}
