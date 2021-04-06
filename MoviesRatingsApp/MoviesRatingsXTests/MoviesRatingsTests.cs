using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using MoviesRatingsApp.Common;
using MoviesRatingsApp.Data;
using MoviesRatingsApp.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace MoviesRatingsXTests
{
    public class MoviesRatingsTests : IClassFixture<WebApplicationFactory<MoviesRatingsApp.Startup>>
    {
        private readonly WebApplicationFactory<MoviesRatingsApp.Startup> _factory;

        private readonly ApplicationDBContext _adc;


        public MoviesRatingsTests(WebApplicationFactory<MoviesRatingsApp.Startup> factory)
        {
            _factory = factory;
            _adc = new ApplicationDBContext(new DbContextOptionsBuilder<ApplicationDBContext>()
                   .UseSqlServer("Persist Security Info=False;User ID=moviedbuser;Password=user123;Initial Catalog=MovieDB;Server=(localdb)\\MSSQLLocalDB;MultipleActiveResultSets=true")
                   .Options);
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/MoviesRatings")]
        public async Task GetMoviesRatingsEndpointsSuccess(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task PostMoviesRatingsEndpointsSuccess()
        {
            var client = _factory.CreateClient();

            var formData = new Dictionary<string, string>();
            formData.Add("Id", "5");
            formData.Add("Rating", "7");

            var formDataContent = new FormUrlEncodedContent(formData);

            var response = await client.PostAsync("/MoviesRatings/Edit/5", formDataContent);

            response.EnsureSuccessStatusCode();

            MoviesRatingsService mrs = new MoviesRatingsService(_adc);
            MovieRating mr = await mrs.GetMovieRatingEntryById(5);
            Assert.True(mr.Rating == 7);


        }
    }

    public class MovieDBTests
    {
        private readonly ApplicationDBContext _adc;

        public MovieDBTests()
        {
            _adc = new ApplicationDBContext(new DbContextOptionsBuilder<ApplicationDBContext>()
                   .UseSqlServer("Persist Security Info=False;User ID=moviedbuser;Password=user123;Initial Catalog=MovieDB;Server=(localdb)\\MSSQLLocalDB;MultipleActiveResultSets=true")
                   .Options);
        }


        [Fact]
        public async Task TestGetMovieRating()
        {
            var mrs = new MoviesRatingsService(_adc);
            var mr = await mrs.GetMovieRatingEntryById(1);
            Assert.NotNull(mr);
        }

        [Fact]
        public async Task TestSaveMovieRating()
        {
            var mrs = new MoviesRatingsService(_adc);
            await mrs.SaveMovieWithRating(1, 5);
            var mr = await mrs.GetMovieRatingEntryById(1);
            Assert.True(mr.Rating == 5);
        }
    }
}
