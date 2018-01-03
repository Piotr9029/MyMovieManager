using System;
using MyMovieManager.Models;
using System.Linq;
using System.Globalization;

namespace MyMovieManager
{
    class CRUD
    {

        public static void Create(OrderContext db, int choose)
        {
            // adding new customer
            if (choose == 1)
            {
                Console.WriteLine("You are adding a new customer.");
                string firstName, lastName;
                Console.Write("First Name: ");
                firstName = Console.ReadLine();
                Console.Write("Last Name: ");
                lastName = Console.ReadLine();
                if (firstName == "" || lastName == "")
                {
                    Console.WriteLine("Input can't be empty, failed to add new record to DB");
                    return;
                }
                else if (firstName.Length > 25 || lastName.Length > 25)
                {
                    Console.WriteLine("Input can't be longer than 25 characters, failed to add new record to DB");
                    return;
                }
                else
                {
                    db.Customers.Add(new Customer { FirstName = firstName, LastName = lastName });
                }

            }

            // adding new movie
            if (choose == 2)
            {
                string title, originalLanguage;
                decimal price;

                try
                {
                    Console.WriteLine("You are adding a new movie.");
                    Console.Write("Title: ");
                    title = Console.ReadLine();
                    Console.Write("Price: ");
                    price = Math.Round(Convert.ToDecimal(Console.ReadLine()), 2);
                    Console.Write("Original Language: ");
                    originalLanguage = Console.ReadLine();
                    if (title == "" || originalLanguage == "")
                    {
                        Console.WriteLine("Input can't be empty, failed to add new record to DB");
                        return;
                    }
                    else if (title.Length > 25 || originalLanguage.Length > 25)
                    {
                        Console.WriteLine("Input can't be longer than 25 characters, failed to add new record to DB");
                        return;
                    }
                    else
                    {
                        db.Movies.Add(new Movie { Title = title, Price = price, OriginalLanguage = originalLanguage });
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Failed to add new record to DB");
                    return;
                }
            }

            // adding new order
            if (choose == 3)
            {
                int movieID, customerID;
                DateTime orderDate;

                try
                {
                    Console.WriteLine("You are adding a new Order.");
                    Console.Write("Customer ID: ");
                    customerID = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Movie ID: ");
                    movieID = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Order Date(pattern dd/mm/yyyy): ");
                    orderDate = Convert.ToDateTime(Console.ReadLine());

                    var customersid = from c in db.Customers select c.CustomerID;
                    var moviesid = from m in db.Movies select m.MovieID;

                    if (moviesid.Contains(movieID) && customersid.Contains(customerID))
                    {
                        db.Orders.Add(new Order { CustomerID = customerID, MovieID = movieID, OrderDate = orderDate });
                    }
                    else
                    {
                        Console.WriteLine("Customer ID:{0} or Movie ID:{1} do not exist in DB", customerID, movieID);
                        return;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Failed to add new record to DB");
                    return;
                }
            }


            db.SaveChanges();
            Console.WriteLine("Succesfully added new record to DB");
        }

        public static void Read(OrderContext db, int choose)
        {
            var count = $"{" ",70 + 5}";

            // reading customers table
            if (choose == 1 || choose == 4)
            {
                for (int i = 0; i < count.Length - 11; i++)
                {
                    Console.Write("_");
                }
                Console.WriteLine();

                var customers = from c in db.Customers select c;
                Console.WriteLine($"|{"CustomerID",10}|{"FirstName",25}|{"LastName",25}|");
                foreach (var customer in customers)
                {
                    Console.WriteLine($"|{customer.CustomerID,10}|{customer.FirstName,25}|{customer.LastName,25}|");
                }
            }

            // reading movies table
            if (choose == 2 || choose == 4)
            {
                for (int i = 0; i < count.Length; i++)
                {
                    Console.Write("_");
                }
                Console.WriteLine();

                var movies = from m in db.Movies select m;
                Console.WriteLine($"|{"MovieID",10}|{"Title",25}|{"OriginalLanguage",25}|{"Price",10}|");
                foreach (var movie in movies)
                {
                    Console.WriteLine($"|{movie.MovieID,10}|{movie.Title,25}|{movie.OriginalLanguage,25}|{Math.Round(movie.Price, 2).ToString("C", new CultureInfo("en-US")),10}|");
                }
            }

            // reading orders table
            if (choose == 3 || choose == 4)
            {
                for (int i = 0; i < count.Length; i++)
                {
                    Console.Write("_");
                }

                Console.WriteLine();

                var orders = from m in db.Orders select m;
                Console.WriteLine($"|{"OrderID",10}|{"CustomerID",25}|{"MovieID",25}|{"OrderDate",10}|");
                foreach (var order in orders)
                {
                    Console.WriteLine($"|{order.OrderID,10}|{order.CustomerID,25}|{order.MovieID,25}|{order.OrderDate.ToString("d"),10}|");
                }
            }
        }

        public static void Update(OrderContext db, int choose)
        {
            int id = 0;
            // updating customer
            if (choose == 1)
            {
                string firstName, lastName;
                try
                {
                    Console.WriteLine("You are updating a customer.\nLeave an empty input and press enter to not change cell");
                    Console.Write("Give an ID of customer that you want to update data of: ");
                    id = Convert.ToInt32(Console.ReadLine());
                    Console.Write("New First Name: ");
                    firstName = Console.ReadLine();
                    Console.Write("New Last Name: ");
                    lastName = Console.ReadLine();
                    if (firstName.Length > 25 || lastName.Length > 25)
                    {
                        Console.WriteLine("Input can't be longer than 25 characters, Failed to update to DB");
                        return;
                    }
                    var customersID = from c in db.Customers select c.CustomerID;
                    if (customersID.Contains(id))
                    {
                        var customer = (from c in db.Customers
                                        where c.CustomerID == id
                                        select c).SingleOrDefault();
                        if (firstName != "") customer.FirstName = firstName;
                        if (lastName != "") customer.LastName = lastName;
                    }
                    else
                    {
                        Console.WriteLine("Customer ID: {0} do not exist in DB. Failed to update to DB", id);
                        return;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Failed to update to DB");
                    return;
                }
            }

            // updating movie
            if (choose == 2)
            {
                string originalLanguage, title, priceString;
                decimal price;
                try
                {
                    Console.WriteLine("You are updating a movie.\nLeave an empty input and press enter to not change cell");
                    Console.Write("Give an ID of movie that you want to update data of: ");
                    id = Convert.ToInt32(Console.ReadLine());
                    Console.Write("New Title: ");
                    title = Console.ReadLine();
                    Console.Write("New Price: ");
                    priceString = Console.ReadLine();
                    Console.Write("New Original Language: ");
                    originalLanguage = Console.ReadLine();

                    if (title.Length > 25 || originalLanguage.Length > 25)
                    {
                        Console.WriteLine("Input can't be longer than 25 characters. Failed to update to DB");
                        return;
                    }

                    var moviesID = from m in db.Movies select m.MovieID;
                    if (moviesID.Contains(id))
                    {
                        var movie = (from m in db.Movies
                                     where m.MovieID == id
                                     select m).SingleOrDefault();

                        if (title != "") movie.Title = title;
                        if (priceString != "")
                        {
                            price = Math.Round(Convert.ToDecimal(priceString), 2);
                            movie.Price = price;
                        }
                        if (originalLanguage != "") movie.OriginalLanguage = originalLanguage;
                    }
                    else
                    {
                        Console.WriteLine("Movie ID:{0} do not exist in DB. Failed to update to DB", id);
                        return;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Failed to update to DB");
                    return;
                }
            }

            // updating order
            if (choose == 3)
            {
                int customerID, movieID;
                string customerStringID, movieStringID, orderDateString;
                DateTime orderDate;
                try
                {
                    Console.WriteLine("You are updating an order.\nLeave an empty input and press enter to not change cell");
                    Console.Write("Give an ID of order that you want to update data of: ");
                    id = Convert.ToInt32(Console.ReadLine());
                    Console.Write("New Customer ID: ");
                    customerStringID = Console.ReadLine();
                    Console.Write("New Movie ID: ");
                    movieStringID = Console.ReadLine();
                    Console.Write("New Order Date(pattern dd/mm/yyyy): ");
                    orderDateString = Console.ReadLine();

                    var ordersID = from o in db.Orders select o.OrderID;
                    if (ordersID.Contains(id))
                    {
                        var order = (from o in db.Orders
                                     where o.OrderID == id
                                     select o).SingleOrDefault();

                        if (customerStringID != "")
                        {
                            customerID = Convert.ToInt32(customerStringID);
                            var customersID = from c in db.Customers select c.CustomerID;
                            if (customersID.Contains(customerID))
                            {
                                order.CustomerID = customerID;
                            }
                            else
                            {
                                Console.WriteLine("Customer ID:{0} does not exist in DB. Failed to update to DB.", customerID);
                                return;
                            }
                        }
                        if (movieStringID != "")
                        {
                            movieID = Convert.ToInt32(movieStringID);
                            var moviesID = from m in db.Movies select m.MovieID;
                            if (moviesID.Contains(movieID))
                            {
                                order.MovieID = movieID;
                            }
                            else
                            {
                                Console.WriteLine("Movie ID:{0} do not exist in DB. Failed to update to DB.", movieID);
                                return;
                            }
                        }
                        if (orderDateString != "")
                        {
                            orderDate = Convert.ToDateTime(orderDateString);
                            order.OrderDate = orderDate;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Order ID:{0} do not exist in DB. Failed to update to DB.", id);
                        return;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Failed to update to DB");
                    return;
                }
            }


            db.SaveChanges();
            Console.WriteLine("Succesfully updated DB");
        }

        public static void Delete(OrderContext db, int choose)
        {
            int id = 0;
            // deleting customer
            if (choose == 1)
            {
                Console.WriteLine("You are deleting a customer.\nBy deleting a customer that is part of Orders table, Orders table will delete all records with following customer");
                Console.Write("Give an ID of customer that you want to delete: ");
                try
                {
                    id = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Failed to delete in DB.");
                    return;
                }
                var customersID = from c in db.Customers select c.CustomerID;
                if (customersID.Contains(id))
                {
                    var customer = db.Customers.Where(c => c.CustomerID == id).FirstOrDefault();
                    db.Customers.Remove(customer);
                }
                else
                {
                    Console.WriteLine("Customer ID:{0} do no exist in DB. Failed to delete in DB", id);
                    return;
                }
            }

            // deleting movie
            if (choose == 2)
            {

                Console.WriteLine("You are deleting a movie. \nIf you delete a movie that is part of Orders table, then Orders table will delete records with following movie");
                Console.Write("Give an ID of movie that you want to delete: ");
                try
                {
                    id = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Failed to delete in DB.");
                    return;
                }
                var moviesID = from m in db.Movies select m.MovieID;
                if (moviesID.Contains(id))
                {
                    var movie = db.Movies.Where(m => m.MovieID == id).FirstOrDefault();
                    db.Movies.Remove(movie);
                }
                else
                {
                    Console.WriteLine("Movie ID:{0} do not exist in DB. Failed to delete in DB", id);
                    return;
                }
            }

            // deleting order
            if (choose == 3)
            {
                Console.WriteLine("You are deleting an order.");
                Console.Write("Give an ID of order that you want to delete: ");
                try
                {
                    id = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Failed to delete in DB.");
                    return;
                }
                var ordersID = from o in db.Orders select o.OrderID;
                if (ordersID.Contains(id))
                {
                    var order = db.Orders.Where(o => o.OrderID == id).FirstOrDefault();
                    db.Orders.Remove(order);
                }
                else
                {
                    Console.WriteLine("Order ID:{0} do not exist in DB.", id);
                    return;
                }
            }

            db.SaveChanges();
            Console.WriteLine("Succesfully deleted the record");
        }
    }
}
