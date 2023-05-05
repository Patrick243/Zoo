using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportApp.Domain;

namespace TransportApp.Application.Repositories
{
    public interface IRouteRepository
    {
        Task<Route> CreateNewRouteAsync(Route route);

        Task<List<Route>> GetListRouteAsync();

        Task<Route> UpdateRouteAsync(Route route);

        Task<Route> DeleteRouteAsync(Route route);

        Task<Route> GetRouteByIdAsync(int id);
    }
}
