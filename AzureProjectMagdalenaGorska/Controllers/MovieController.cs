using AzureProjectMagdalenaGorska.Models;
using AzureProjectMagdalenaGorska.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzureProjectMagdalenaGorska.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        public readonly IMovieCosmosService _movieCosmosService;
        public MovieController(IMovieCosmosService movieCosmosService)
        {
            _movieCosmosService = movieCosmosService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var sqlCosmosQuery = "Select * from c";
            var result = await _movieCosmosService.Get(sqlCosmosQuery);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Movie newMovie)
        {
            newMovie.Id = Guid.NewGuid().ToString();
            var result = await _movieCosmosService.AddAsync(newMovie);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Movie movieToUpdate)
        {
            var result = await _movieCosmosService.Update(movieToUpdate);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id, string type)
        {
            await _movieCosmosService.Delete(id, type);
            return Ok();
        }
    }
}
