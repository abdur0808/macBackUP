﻿using System;
using System.Linq.Expressions;
using VillaAPI.models;

namespace VillaAPI.Repository
{
	public interface IRepository<T> where T: class
	{
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);

        Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true);

        Task CreateAsync(T entity);

        Task RemoveAsync(T entity);

        Task SaveAsync();
    }
}

