using System.Collections.Generic;

namespace MoviesRatingsApp.ViewModels
{
    public class Movie : SWAPICommon
    {
        public string title { get; set; }
        public int episode_id { get; set; }
        public string opening_crawl { get; set; }
        public string director { get; set; }
        public string producer { get; set; }
        public string release_date { get; set; }
        public IEnumerable<string> characters { get; set; }
        public IEnumerable<string> planets { get; set; }
        public IEnumerable<string> starships { get; set; }
        public IEnumerable<string> vehicles { get; set; }
        public IEnumerable<string> species { get; set; }






    }
}
