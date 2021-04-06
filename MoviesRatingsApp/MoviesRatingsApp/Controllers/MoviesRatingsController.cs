using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoviesRatingsApp.Common;
using MoviesRatingsApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesRatingsApp.Controllers
{
    public class MoviesRatingsController : Controller
    {
        private readonly ISWAPIService _swapiService;
        private readonly IMapper _mapper;
        private readonly IMoviesRatingsService _movieRatingsService;
        private readonly ILogger<MoviesRatingsController> _logger;

        public IEnumerable<Movie> SWAPIMovies { get; private set; }
        public IEnumerable<ViewMovie> ViewMoviesWithRatings { get; private set; }

        public Movie SWAPIMovie { get; private set; }
        public ViewMovie ViewMovieWithRating { get; private set; }


        public MoviesRatingsController(ISWAPIService swapiService, IMapper mapper, IMoviesRatingsService movieRatingsService, ILogger<MoviesRatingsController> logger)
        {
            _swapiService = swapiService;
            _mapper = mapper;
            _logger = logger;
            _movieRatingsService = movieRatingsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            SWAPIMovies = await _swapiService.GetMovies();

            ViewMoviesWithRatings = _mapper.Map<IEnumerable<Movie>, IEnumerable<ViewMovie>>(SWAPIMovies);
            foreach (ViewMovie ViewMovieWRating in ViewMoviesWithRatings)
            {
                var MovieRatingEntry = await _movieRatingsService.GetMovieRatingEntryById(ViewMovieWRating.Id);
                if (MovieRatingEntry != null)
                {
                    ViewMovieWRating.Rating = MovieRatingEntry.Rating;
                }

            }

            return View(ViewMoviesWithRatings);
        }

        [HttpGet]
        // GET: MoviesRatings/Edit/5
        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            SWAPIMovie = await _swapiService.GetMovie(Id.Value);

            ViewMovieWithRating = _mapper.Map<Movie, ViewMovie>(SWAPIMovie);
            var MovieRatingEntry = await _movieRatingsService.GetMovieRatingEntryById(ViewMovieWithRating.Id);

            if (MovieRatingEntry != null)
            {
                ViewMovieWithRating.Rating = MovieRatingEntry.Rating;
            }

            List<Task> lt = new();
            ViewMovieWithRating.CharactersList = new();
            ViewMovieWithRating.PlanetsList = new();
            ViewMovieWithRating.StarshipsList = new();
            ViewMovieWithRating.VehiclesList = new();
            ViewMovieWithRating.SpeciesList = new();

            foreach (string item in ViewMovieWithRating.characters)
            {
                lt.Add(Task.Run(async () =>
                {
                    ViewMovieWithRating.CharactersList.Add(await _swapiService.GetPeople(item));
                }));
            }
            foreach (string item in ViewMovieWithRating.planets)
            {
                lt.Add(Task.Run(async () =>
                {
                    ViewMovieWithRating.PlanetsList.Add(await _swapiService.GetPlanet(item));
                }));
            }
            foreach (string item in ViewMovieWithRating.starships)
            {
                lt.Add(Task.Run(async () =>
                {
                    ViewMovieWithRating.StarshipsList.Add(await _swapiService.GetStarship(item));
                }));
            }
            foreach (string item in ViewMovieWithRating.vehicles)
            {
                lt.Add(Task.Run(async () =>
                {
                    ViewMovieWithRating.VehiclesList.Add(await _swapiService.GetVehicle(item));
                }));
            }
            foreach (string item in ViewMovieWithRating.species)
            {
                lt.Add(Task.Run(async () =>
                {
                    ViewMovieWithRating.SpeciesList.Add(await _swapiService.GetSpecies(item));
                }));
            }

            await Task.WhenAll(lt);

            return View(ViewMovieWithRating);
        }

        // POST: MoviesRatings/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Rating")] ViewMovie viewMovieWithRating)
        {
            if (id != viewMovieWithRating.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _movieRatingsService.SaveMovieWithRating(viewMovieWithRating.Id, viewMovieWithRating.Rating);

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "DB Saving issue");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(viewMovieWithRating);
        }


    }
}
