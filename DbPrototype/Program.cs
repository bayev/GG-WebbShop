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
        }
        static void SeedDB()
        {
            Product productOne = new Product();
            productOne.Name = "Ralph Lauren - Black T-shirt";
            productOne.Price = 799; //Sek
            productOne.Weight = 200; //gram
            productOne.Description = "T-shirt with a classic black design";
            productOne.Image = null;
            productOne.Category = "T-shirt";
            productOne.CreateDate = DateTime.Now;
            productOne.Stock = 15;
            productOne.Size = "S";

            Product productTwo = new Product();
            productTwo.Name = "Hugo Boss - Blue Blazer Jacket";
            productTwo.Price = 1999; //Sek
            productTwo.Weight = 500; //gram
            productTwo.Description = "A timeless blazer, handsewn in Mexico";
            productTwo.Image = null;
            productTwo.Category = "Blazer";
            productTwo.CreateDate = DateTime.Now;
            productTwo.Stock = 10;
            productTwo.Size = "M";

            Product productThree = new Product();
            productThree.Name = "Morris - Black/blue Sweater";
            productThree.Price = 899; //Sek
            productThree.Weight = 550; //gram
            productThree.Description = "The sweater you've allways dreamed of. 100% cotton";
            productThree.Image = null;
            productThree.Category = "Sweater";
            productThree.CreateDate = DateTime.Now;
            productThree.Stock = 6;
            productThree.Size = "L";

            Product productFour = new Product();
            productFour.Name = "Levis - Blue Jeans";
            productFour.Price = 1299; //Sek
            productFour.Weight = 670; //gram
            productFour.Description = "The blue jeans that you can do whatever you want in, they'll endure";
            productFour.Image = null;
            productFour.Category = "Jeans";
            productFour.CreateDate = DateTime.Now;
            productFour.Stock = 10;
            productFour.Size = "M";

            List<Product> productList = new List<Product>();
            productList.Add(productOne);
            productList.Add(productTwo);
            productList.Add(productThree);
            productList.Add(productFour);

            using (var context = new AppDbContext())
            {
                foreach (var item in productList)
                {
                    context.Add(item);
                }

                context.SaveChanges();
            }
        }
    }
}
