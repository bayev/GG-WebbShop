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

                Console.Write("Brand : ");
                product1.Brand = Console.ReadLine();

                product1.CreateDate = DateTime.Now;

                Console.Write("How many : ");
                product1.Stock = int.Parse(Console.ReadLine());

                Console.Write("Size : ");
                product1.Size = Console.ReadLine();

                context.Add(product1);
                context.SaveChanges();
            }
        }
        public static List<Product> ReadProducts()
        {
            List<Product> products = new List<Product>();

            using (var context = new AppDbContext())
            {
                var resultData = context.Products;

                foreach (var item in resultData)
                {
                    Console.WriteLine($"\nID:{item.Id} || Name:{item.Name} || Price:{item.Price} || Stock: {item.Stock}");
                    products.Add(item);
                }
            }

            return products;
        }
        public static void UpdateProduct()
        {
            Console.Write("Ange ProduktID att uppdatera: ");
            int id = int.Parse(Console.ReadLine());
            using (var context = new AppDbContext())
            {
                var result = context.Products.SingleOrDefault(x => x.Id == id);

                if (result != null)
                {
                    Console.Write("New name: ");
                    result.Name = Console.ReadLine();
                    Console.Write("New price: ");
                    result.Price = decimal.Parse(Console.ReadLine());
                    Console.Write("New Brand: ");
                    result.Brand = Console.ReadLine();
                    Console.Write("New Description: ");
                    result.Description = Console.ReadLine();
                    result.Image = null;
                    Console.Write("New Category: ");
                    result.Category = Console.ReadLine();
                    Console.WriteLine("Date have been updated....");
                    result.CreateDate = DateTime.Now;
                    Console.Write("New Stock: ");
                    result.Stock = int.Parse(Console.ReadLine());
                    Console.Write("New Size: ");
                    result.Size = Console.ReadLine();

                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("No product with this ID exists in the database");
                }
            }
        }
        public static void DeleteProduct(int InputId)
        {
            using (var context = new AppDbContext())
            {
                var resultData = context.Products
                    .Where(q => q.Id == InputId)
                    .FirstOrDefault();

                context.Products.Remove(resultData);

                Console.WriteLine($"Delted: {resultData.Id} || {resultData.Name}");

                context.SaveChanges();
            }
        }
    }
}
