using Microsoft.EntityFrameworkCore;
using MoviesRatingsApp.Models;

namespace MoviesRatingsApp.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
        }

        public DbSet<MovieRating> MovieRatings { get; set; }
    }
}
