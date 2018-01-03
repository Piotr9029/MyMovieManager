using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MyMovieManager.Models;
using Newtonsoft.Json;

namespace MyMovieManager
{
    class DbInitializer
    {
        public static void Initialize(OrderContext db)
        {

            try
            {
                db.Database.EnsureCreated();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                Console.WriteLine("\nYou need Microsoft sql localdb server with MSSQLocalDB server name for this program to work.\nPress key to exit program.");
                Console.ReadKey();
                Environment.Exit(0);
            }

            if (db.Movies.Any())
            {
                return;
            }

            // if movies table is empty
            // loading sample data from json file
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "SampleData.json");
            if (File.Exists(filePath))
            {
                Console.WriteLine("Loading sample data from {0}", filePath);
                var json = File.ReadAllText(filePath);
                var movies = JsonConvert.DeserializeObject<IEnumerable<Movie>>(json);
                db.Movies.AddRange(movies);
            }
            // Loading this if json file is missing
            else
            {
                Console.WriteLine("{0} is missing, getting backup sample data", filePath);
                var movie = new Movie { Title = "Night of the Lepus", Price = 50, OriginalLanguage = "English" };
                db.Movies.Add(movie);
                var customer = new Customer { FirstName = "Alex", LastName = "Josh" };
                db.Customers.Add(customer);
                var order = new Order { MovieID = 1, CustomerID = 1, OrderDate = DateTime.Now };
                db.Orders.Add(order);
            }

            db.SaveChanges();
        }
    }
}