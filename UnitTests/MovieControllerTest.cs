using AzureProjectMagdalenaGorska.Controllers;
using AzureProjectMagdalenaGorska.Models;
using AzureProjectMagdalenaGorska.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.ServicesFake;

namespace UnitTests
{
    public class MovieControllerTest
    {
        private readonly MovieController _controller;
        private readonly IMovieCosmosService _movieCosmosService;

        public MovieControllerTest()
        {
            _movieCosmosService = new MovieCosmosServiceFake();
            _controller = new MovieController(_movieCosmosService);
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsOkResultAsync()
        {
            // Act
            var okResult = await _controller.Get();

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsAllItems()
        {
            // Act
            OkObjectResult okResult = await _controller.Get() as OkObjectResult;

            // Assert
            var items = Assert.IsType<List<Movie>>(okResult.Value);
            Assert.Equal(3, items.Count);
        }

        [Fact]
        public async Task Post_WhenCalled_ReturnsOkResultAsync()
        {
            // Arrange
            Movie movie = new Movie() { Id = Guid.NewGuid().ToString(), Director = "anana", Title = "SKJSKSK", Type = "dkdkd" };

            // Act
            var okResult = await _controller.Post(movie);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task Post_WhenCalled_ReturnsAddedItem()
        {
            // Arrange
            Movie movie = new Movie() { Id = Guid.NewGuid().ToString(), Director = "anana", Title = "SKJSKSK", Type = "dkdkd" };

            // Act
            OkObjectResult okResult = await _controller.Post(movie) as OkObjectResult;

            // Assert
            var item = Assert.IsType<Movie>(okResult.Value);
            AssertObjectsAreEquivalents(item, movie);
        }

        [Fact]
        public async Task Put_WhenCalled_ReturnsOkResultAsync()
        {
            // Arrange
            Movie movie = new Movie() { Id = Guid.NewGuid().ToString(), Director = "anana", Title = "SKJSKSK", Type = "dkdkd" };
            var okPostResult = await _controller.Post(movie);
            Movie alteredMovie = new Movie() { Id = movie.Id, Director = "CHANGEDIRECTOR", Title = "SKJSKSK", Type = "dkdkd" };

            // Act
            var okPutResult = await _controller.Put(alteredMovie);

            // Assert
            Assert.IsType<OkObjectResult>(okPutResult);
        }

        [Fact]
        public async Task Put_WhenCalled_ReturnsAlteredItem()
        {
            // Arrange
            Movie movie = new Movie() { Id = Guid.NewGuid().ToString(), Director = "anana", Title = "SKJSKSK", Type = "dkdkd" };
            var okPostResult = await _controller.Post(movie);
            Movie alteredMovie = new Movie() { Id = movie.Id, Director = "CHANGEDIRECTOR", Title = "SKJSKSK", Type = "dkdkd" };

            // Act
            OkObjectResult okPutResult = await _controller.Put(alteredMovie) as OkObjectResult;

            // Assert
            var item = Assert.IsType<Movie>(okPutResult.Value);
            AssertObjectsAreEquivalents(item, alteredMovie);
        }

        [Fact]
        public async Task Delete_WhenCalled_ReturnsOkResultAsync()
        {
            // Arrange
            Movie movie = new Movie() { Id = Guid.NewGuid().ToString(), Director = "anana", Title = "SKJSKSK", Type = "dkdkd" };
            var okPostResult = await _controller.Post(movie);

            // Act
            var okResult = await _controller.Delete(movie.Id, movie.Type);

            // Assert
            Assert.IsType<OkResult>(okResult);
        }

        [Fact]
        public async Task Delete_WhenCalled_DeletesAnItem()
        {
            // Arrange
            Movie movie = new Movie() { Id = Guid.NewGuid().ToString(), Director = "anana", Title = "SKJSKSK", Type = "dkdkd" };
            var okPostResult = await _controller.Post(movie);
            OkObjectResult okGetResultBefore = await _controller.Get() as OkObjectResult;
            var itemsBefore = Assert.IsType<List<Movie>>(okGetResultBefore.Value);
            Assert.Equal(4, itemsBefore.Count);


            // Act
            var okResult = await _controller.Delete(movie.Id, movie.Type);

            // Assert
            OkObjectResult okGetResultAfter = await _controller.Get() as OkObjectResult;
            var itemsAfter = Assert.IsType<List<Movie>>(okGetResultAfter.Value);
            Assert.Equal(3, itemsAfter.Count);
        }

        private void AssertObjectsAreEquivalents(Movie first, Movie second)
        {
            Assert.Equal(first.Id, second.Id);
            Assert.Equal(first.Title, second.Title);
            Assert.Equal(first.Type, second.Type);
            Assert.Equal(first.Director, second.Director);
        }
    }
}
