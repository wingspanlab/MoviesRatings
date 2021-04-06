using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesRatingsApp.ViewModels
{
    public class ViewMovie : Movie
    {
        public int Id { get; set; }

        [Range(1, 10,
         ErrorMessage = "Rating must be between 1 and 10")]
        [Required(ErrorMessage = "Please provide rating")]
        public int Rating { get; set; }
        public List<People> CharactersList { get; set; }
        public List<Planet> PlanetsList { get; set; }
        public List<Starship> StarshipsList { get; set; }
        public List<Vehicle> VehiclesList { get; set; }
        public List<Species> SpeciesList { get; set; }

    }


}
