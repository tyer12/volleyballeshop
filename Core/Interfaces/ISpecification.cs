using System;
using System.Linq.Expressions;

namespace Core.Interfaces;

public interface ISpecification<T>
{
    Expression<Func<T, bool>>? Criteria { get; }
    Expression<Func<T, object>>? OrderBy { get; }
    Expression<Func<T, object>>? OrderByDescending { get; }
    bool IsDistinct { get; }
    int Take { get; }
    int Skip { get; }
    bool IsPagingEnabled { get; }
    IQueryable<T> ApplyCriteria(IQueryable<T> query);
    // Other properties and methods can be added here as needed, such as Includes, OrderBy, etc.
}

public interface ISpecification<T, Tresult> : ISpecification<T>
{
    Expression<Func<T, Tresult>>? Select { get; }
}


