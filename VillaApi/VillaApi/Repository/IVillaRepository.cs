using System;
using System.Linq.Expressions;
using VillaAPI.models;

namespace VillaAPI.Repository
{
	public interface IVillaRepository : IRepository<Villa>
	{

        Task<Villa> UpdateAsync(Villa entity);
    }


}

