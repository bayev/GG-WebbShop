using System;
using System.Linq;
using System.Collections.Generic;
using DbPrototype.Models.Entities;

namespace DbPrototype
{
    class Program
    {
        static void Main(string[] args)
        {
            //CRUD.CreateProduct();
            CRUD.ReadProducts();
            CRUD.UpdateProduct();
            //CRUD.DeleteProduct(int.Parse(Console.ReadLine()));

            //SeedDB();
        }

        
    }
}
