using System;
using VillaWebApp.Models;

namespace VillaWebApp.Services.IServices
{
	public interface IVillaService
	{
		Task<T> GetAllAsync<T>();

		Task<T> GetAsync<T>(int id);

		Task<T> CreateAsync<T>(VillaDTO dto);

        Task<T> UpdateAsync<T>(VillaDTO dto);

        Task<T> DeleteAsync<T>(int id);
    }
}

