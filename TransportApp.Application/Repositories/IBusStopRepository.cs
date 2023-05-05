using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportApp.Domain;

namespace TransportApp.Application.Repositories
{
    public interface IBusStopRepository
    {
        Task<BusStop> CreateNewBusStopAsync(BusStop busStop);

        Task<List<BusStop>> GetListBusStopAsync();

        Task<BusStop> UpdateBusStopAsync(BusStop busStop);

        Task<BusStop> DeleteBusStopAsync(BusStop busStop);

        Task<BusStop> GetBusStopByIdAsync(int id);
    }
}
