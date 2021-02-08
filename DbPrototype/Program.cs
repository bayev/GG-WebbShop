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
            //SeedDB();
            //TODO: Change datetime properties for ID 1 and 2.
        }
        static void SeedDB()
        {
            using (var context = new AppDbContext())
            {

                Product product = new Product();
                product.Name = "Ralph Lauren - Black T-shirt";
                product.Price = 799; //Sek
                product.Weight = 200; //gram
                product.Description = "T-shirt with a classic black design";
                product.Image = null;
                product.Category = "T-shirt";
                product.CreateDate = DateTime.Now;
                product.Stock = 10;
                product.Size = "S";
                context.Add(product);



                context.SaveChanges();
            }
        }
    }
}
