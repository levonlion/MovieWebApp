using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWebApp.Models
{
    public class ActorModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LikeCount { get; set; }
        public List<MovieModel> Movies { get; set; } = new List<MovieModel>();
        public bool IsLiked { get; set; }

    }
}
