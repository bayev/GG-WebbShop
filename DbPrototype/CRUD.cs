using DbPrototype.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DbPrototype
{
    public class CRUD
    {
        public static void CreateProduct()
        {
            using (var context = new AppDbContext())
            {
                Product product1 = new Product();

                Console.WriteLine("**********Enter Product details**********");
                Console.WriteLine("----------------------------------------");
                Console.Write("Product Name : ");
                product1.Name = Console.ReadLine();

                Console.Write("Product Price : ");
                product1.Price = decimal.Parse(Console.ReadLine());

                Console.Write("Product Weight : ");
                product1.Weight = decimal.Parse(Console.ReadLine());

                Console.Write("Product Description : ");
                product1.Description = Console.ReadLine();

                product1.Image = null;

                Console.Write("Category : ");
                product1.Category = Console.ReadLine();

                product1.CreateDate = DateTime.Now;

                Console.Write("How many : ");
                product1.Stock = int.Parse(Console.ReadLine());

                Console.Write("Size : ");
                product1.Size = Console.ReadLine();

                context.Add(product1);
                context.SaveChanges();
            }
        }
        public static List<Product> ReadProduct()
        {
            List<Product> products = new List<Product>();

            using (var context = new AppDbContext())
            {
                var resultData = context.Products;

                foreach (var item in resultData)
                {
                    products.Add(item);
                }
            }

            return products;
        }
    }
}
