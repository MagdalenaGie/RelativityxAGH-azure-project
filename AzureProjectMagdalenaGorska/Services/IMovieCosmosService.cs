using AzureProjectMagdalenaGorska.Models;

namespace AzureProjectMagdalenaGorska.Services
{
    public interface IMovieCosmosService
    {
        Task<List<Movie>> Get(string sqlCosmosQuery);
        Task<Movie> AddAsync(Movie newMovie);
        Task<Movie> Update(Movie movieToUpdate);
        Task Delete(string id, string type);
    }
}
