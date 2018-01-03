using System;
using MyMovieManager.Models;

namespace MyMovieManager
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("This is a program for CRUD(Create,Read,Update,Delete) operations on local database MyMovieManager. " +
                "If Database is missing the program automatically will generate .mdf and .log files by default on C:\\Users\\<User>\n" +
                "You have got 3 tables available: Customers, Movies and Orders.");
            using (var db = new OrderContext())
            {
                DbInitializer.Initialize(db);

                string s;
                Console.WriteLine("What operation would you like to do on database?");
                while (true)
                {
                    Console.WriteLine("1-Create, 2-Read, 3-Update, 4-Delete, 5 to exit program");
                    s = Console.ReadLine();
                    switch (s)
                    {
                        case "1":
                            InvokeOperations.InvokeCRUD(db, CRUD.Create);
                            break;
                        case "2":
                            InvokeOperations.InvokeCRUD(db, CRUD.Read);
                            break;
                        case "3":
                            InvokeOperations.InvokeCRUD(db, CRUD.Update);
                            break;
                        case "4":
                            InvokeOperations.InvokeCRUD(db, CRUD.Delete);
                            break;
                        case "5":
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Choose between 5 options: 1-Create, 2-Read, 3-Update, 4-Delete, 5 to exit program");
                            break;
                    }
                }
            }
        }
    }
}
