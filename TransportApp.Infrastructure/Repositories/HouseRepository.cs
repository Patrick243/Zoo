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
    public class HouseRepository : IHouseRepository
    {
        private ApplicationDbContext _db;

        public HouseRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<House> CreateNewHouseAsync(House house)
        {
            await _db.Houses.AddAsync(house);
            await _db.SaveChangesAsync();
            return house;
        }

        public async Task<House> DeleteHouseAsync(House house)
        {
            if (house != null)
            {
                _db.Houses.Remove(house);
                await _db.SaveChangesAsync();

                return house;
            }
            return null;
        }

        public async Task<House> GetHouseByIdAsync(int id)
        {
            var houses = await _db.Houses.Include(x => x.Street).FirstOrDefaultAsync(x => x.Id == id);
            if (houses != null)
            {
                return houses;
            }
            return null;
        }

        public async Task<List<House>> GetListHouseAsync()
        {
            var houses = await _db.Houses.Include(x => x.Street).ToListAsync();
            if (houses != null)
            {
                return houses;
            }
            return null;
        }

        public async Task<House> UpdateHouseAsync(House house)
        {
            if (house != null)
            {
                _db.Houses.Update(house);
                await _db.SaveChangesAsync();
                return house;
            }
            return null;
        }
    }
}
