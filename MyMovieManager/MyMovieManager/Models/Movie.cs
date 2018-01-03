using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMovieManager.Models
{
    class Movie
    {
        public int MovieID { get; set; }
        [MaxLength(25)]
        public string Title { get; set; }
        public decimal Price { get; set; }
        [MaxLength(25)]
        public string OriginalLanguage { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
