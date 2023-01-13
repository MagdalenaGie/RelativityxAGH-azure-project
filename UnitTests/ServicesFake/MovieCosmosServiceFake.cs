using AzureProjectMagdalenaGorska.Models;
using AzureProjectMagdalenaGorska.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.ServicesFake
{
    internal class MovieCosmosServiceFake : IMovieCosmosService
    {
        private readonly List<Movie> _moviesList;

        public MovieCosmosServiceFake()
        {
            _moviesList = new List<Movie>()
            {
                new Movie(){ Id = Guid.NewGuid().ToString(), Director = "anana", Title= "SKJSKSK", Type="dkdkd"},
                new Movie(){ Id = Guid.NewGuid().ToString(), Director = "anana", Title= "SKJSKSK", Type="dkdkd"},
                new Movie(){ Id = Guid.NewGuid().ToString(), Director = "anana", Title= "SKJSKSK", Type="dkdkd"}
            };
        }
        public Task<Movie> AddAsync(Movie newMovie)
        {
            _moviesList.Add(newMovie);
            return Task.FromResult(newMovie);
        }

        public Task Delete(string id, string type)
        {
            if (_moviesList.Find(x => x.Id == id && x.Type == type) != null)
            {
                Movie toDelete = _moviesList.Find(x => x.Id == id && x.Type == type) ?? new Movie();
                _moviesList.Remove(toDelete);
            }
            
            return Task.CompletedTask;
        }

        public Task<List<Movie>> Get(string sqlCosmosQuery)
        {
            return Task.FromResult(_moviesList);
        }

        public Task<Movie> Update(Movie movieToUpdate)
        {
            int index = _moviesList.FindIndex(x => x.Id == movieToUpdate.Id);
            _moviesList[index] = movieToUpdate;
            return Task.FromResult(_moviesList[index]);
        }
    }
}
