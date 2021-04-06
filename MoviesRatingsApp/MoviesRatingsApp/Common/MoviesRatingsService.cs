using Microsoft.EntityFrameworkCore;
using MoviesRatingsApp.Data;
using MoviesRatingsApp.Models;
using System.Threading.Tasks;

namespace MoviesRatingsApp.Common
{
    public interface IMoviesRatingsService
    {
        Task<MovieRating> GetMovieRatingEntryById(int Id);
        Task SaveMovieWithRating(int id, int rating);
    }
    public class MoviesRatingsService : IMoviesRatingsService
    {
        private readonly ApplicationDBContext _context;
        public MoviesRatingsService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<MovieRating> GetMovieRatingEntryById(int Id)
        {
            var MovieRatingEntry = await _context.MovieRatings
                .FirstOrDefaultAsync(m => m.MovieID == Id);
            if (MovieRatingEntry != null)
            {
                return MovieRatingEntry;
            }

            return null;
        }

        public async Task SaveMovieWithRating(int id, int rating)
        {
            var MovieRatingEntry = await _context.MovieRatings
                .FirstOrDefaultAsync(m => m.MovieID == id);
            if (MovieRatingEntry == null)
            {
                MovieRatingEntry = new MovieRating();
                MovieRatingEntry.MovieID = id;
                MovieRatingEntry.Rating = rating;
                _context.Add(MovieRatingEntry);
                await _context.SaveChangesAsync();

            }
            else
            {
                MovieRatingEntry.Rating = rating;
                _context.Update(MovieRatingEntry);
            }

            await _context.SaveChangesAsync();
        }
    }
}
