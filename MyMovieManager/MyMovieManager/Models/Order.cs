using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMovieManager.Models
{
    class Order
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public int MovieID { get; set; }
        public DateTime OrderDate { get; set; }

        public Customer Customer { get; set; }
        public Movie Movie { get; set; }
    }
}
