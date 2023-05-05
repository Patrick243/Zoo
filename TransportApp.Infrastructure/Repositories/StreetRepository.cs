using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportApp.Application.Repositories;
using TransportApp.Domain;

namespace TransportApp.Infrastructure.Repositories
{
    public class StreetRepository : IStreetRepository
    {
        private readonly ApplicationDbContext _db;

        public StreetRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Street> CreateNewStreetAsync(Street street)
        {
            await _db.Streets.AddAsync(street);
            await _db.SaveChangesAsync();
            return street;
        }

        public async Task<Street> DeleteStreetAsync(Street street)
        {
            if (street != null)
            {
                _db.Streets.Remove(street);
                await _db.SaveChangesAsync();

                return street;
            }
            return null;
        }

        public async Task<List<Street>> GetListStreetAsync()
        {
            var street = await _db.Streets.Include(x => x.Houses).Include(x => x.BusStop).ToListAsync();
            if (street != null)
            {
                return street;
            }
            return null;
        }

        public async Task<Street> GetStreetByIdAsync(int id)
        {
            var street = await _db.Streets.Include(x => x.Houses).Include(x => x.BusStop).FirstOrDefaultAsync(x => x.Id == id);
            if (street != null)
            {
                return street;
            }
            return null;
        }

        public async Task<Street> UpdateStreetAsync(Street street)
        {
            if (street != null)
            {
                _db.Streets.Update(street);
                await _db.SaveChangesAsync();
                return street;
            }
            return null;
        }
    }
}
