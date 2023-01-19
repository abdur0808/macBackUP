using System;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VillaAPI.Data;
using VillaAPI.Logger;
using VillaAPI.models;

namespace VillaAPI.Repository
{
	public class VillaRepositoty : Repository<Villa>, IVillaRepository
	{
        
        private readonly ApplicationDbContext _db;
        public VillaRepositoty(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public async Task<Villa> UpdateAsync(Villa entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.Villas.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}

