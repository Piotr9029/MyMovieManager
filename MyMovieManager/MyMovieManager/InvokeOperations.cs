using System;
using System.Linq;
using MyMovieManager.Models;

namespace MyMovieManager
{
    class InvokeOperations
    {
        public static void InvokeCRUD(OrderContext db, Action<OrderContext, int> MyCRUDMethod)
        {
            string choose;
            string[] chooseTable;

            if (MyCRUDMethod == CRUD.Read)
            {
                chooseTable = new string[] { "1", "2", "3", "4" };
                Console.WriteLine("1-Customers, 2-Movies, 3-Orders, 4-All Tables");
            }
            else
            {
                chooseTable = new string[] { "1", "2", "3" };
                Console.WriteLine("1-Customers, 2-Movies, 3-Orders");
            }
            while (true)
            {
                choose = Console.ReadLine();
                if (chooseTable.Contains(choose))
                {
                    MyCRUDMethod(db, Convert.ToInt32(choose));
                    break;
                }
                else
                {
                    if (MyCRUDMethod == CRUD.Read)
                    {
                        Console.WriteLine("choose between 4 options: 1-Customers, 2-Movies, 3-Orders, 4-All Tables");
                    }
                    else
                    {
                        Console.WriteLine("choose between 3 options: 1-Customers, 2-Movies, 3-Orders");
                    }
                }
            }
        }
    }
}
