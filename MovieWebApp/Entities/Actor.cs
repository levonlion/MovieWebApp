using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWebApp.Entities
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ActorMovie> ActorMovies { get; set; }
        public ICollection<ActorRating> ActorRatings { get; set; }
    }
}
