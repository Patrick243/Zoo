
using TransportApp.Domain;

namespace TransportApp.Application.Repositories
{
    public interface IStreetRepository
    {
        Task<Street> CreateNewStreetAsync(Street street);

        Task<List<Street>> GetListStreetAsync();

        Task<Street> UpdateStreetAsync(Street street);

        Task<Street> DeleteStreetAsync(Street street);

        Task<Street> GetStreetByIdAsync(int id);
    }
}
