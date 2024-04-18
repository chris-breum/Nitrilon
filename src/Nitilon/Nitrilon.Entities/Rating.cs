using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nitrilon.Entities
{
    public class Rating
    {
        private int id;
        private int ratingvalue;
        private string description;

        public Rating(int id, int ratingvalue, string description)
        {
            Id = id;
            Ratingvalue = ratingvalue;
            Description = description;
        }

        public int Id { get => id; set => id = value; }
        public int Ratingvalue { get => ratingvalue; set => ratingvalue = value; }
        public string Description { get => description; set => description = value; }
    }
}
