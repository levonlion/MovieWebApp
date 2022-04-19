using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWebApp.Models
{
    public class MovieModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int LikeCount { get; set; }
        public List<ActorModel> Actors { get; set; } = new List<ActorModel>();
        public bool IsLiked { get; set; }
    }
}

