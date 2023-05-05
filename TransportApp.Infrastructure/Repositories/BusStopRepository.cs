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
    public class BusStopRepository : IBusStopRepository
    {
        private readonly ApplicationDbContext _db;

        public BusStopRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<BusStop> CreateNewBusStopAsync(BusStop busStop)
        {
            await _db.BusStops.AddAsync(busStop);
            _db.SaveChanges();
            return busStop;
        }

        public async Task<BusStop> DeleteBusStopAsync(BusStop busStop)
        {
            if (busStop != null)
            {
                _db.BusStops.Remove(busStop);
                await _db.SaveChangesAsync();
            }
            return null;
        }

        public async Task<List<BusStop>> GetListBusStopAsync()
        {
            var busStops = await _db.BusStops.Include(x => x.Route).Include(x => x.Streets)
                .ThenInclude(x => x.Houses).ToListAsync();
       
            if (busStops != null)
            {
                return busStops;
            }
            return null;
        }

        public async Task<BusStop> UpdateBusStopAsync(BusStop busStop)
        {
            if (busStop != null)
            {
                _db.BusStops.Update(busStop);
                await _db.SaveChangesAsync();
            }
            return null;
        }

        public async Task<BusStop> GetBusStopByIdAsync(int id)
        {
            var busStop = await _db.BusStops.Include(x => x.Route).Include(x =>x.Streets)
                .ThenInclude(x => x.Houses).FirstOrDefaultAsync(x => x.Id == id);
            if (busStop != null)
            {
                return busStop;
            }
            return null;
        }
    }
}
