using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab17Exercitii.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab17Exercitii
{
    public class ProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public string AddProduct(string name, int stock, int manufacturerId, int categoryId, Guid barcode, double price)
        {
            
            var existingProduct = _context.Products.FirstOrDefault(p => p.Name == name);
            if (existingProduct != null)
            {
               return $"The product '{existingProduct.Name}' is already in the database.";
            }
            
            var product = new Product
            {
                Name = name,
                Stock = stock,
                ManufacturerId = manufacturerId,
                CategoryId = categoryId,
                Tags = new List<Tag>
                {
                    new Tag { Barcode = barcode, Price = price }
                }
            };
            _context.Products.Add(product);
            _context.SaveChanges();

            return $"The product '{name}' was added.";
        }

        public void UpdateProductPrice(int productId, double newPrice)
        {
            var product = _context.Products.Include(p => p.Tags).FirstOrDefault(p => p.Id == productId);
            if (product != null)
            {
                foreach (var tag in product.Tags)
                {
                    tag.Price = newPrice;
                }
                _context.SaveChanges();
                Console.WriteLine($"Price updated for product '{product.Name}' (ID: {product.Id}) to {newPrice:C2}.");
            }
        }

        public double GetTotalStockValue()
        {
            var productsWithTags = _context.Products.Include(p => p.Tags).ToList();
            var totalValue = productsWithTags.Sum(p => p.Stock * p.Tags.First().Price);

            return totalValue;
        }

        public double GetTotalStockValueByManufacturer(int manufacturerId)
        {
           var productsWithTags = _context.Products
                .Include(p => p.Tags)
                .Where(p => p.ManufacturerId == manufacturerId)
                .ToList();
            var totalValue = productsWithTags.Sum(p => p.Stock * p.Tags.First().Price);
            return totalValue;
        }

        public double GetTotalStockValueByCategory(int categoryId)
        {
            var productsWithTags = _context.Products
                .Include(p => p.Tags)
                .Where(p => p.CategoryId == categoryId)
                .ToList();
            var totalValue = productsWithTags.Sum(p => p.Stock * p.Tags.First().Price);
            return totalValue;
        }
        public double GetTotalStockValueByCategoryAndManufacturer(int categoryId, int manufacturerId)
        {
            var products = _context.Products
                .Include(p => p.Tags)
                .Where(p => p.CategoryId == categoryId && p.ManufacturerId == manufacturerId)
                .ToList();
            var totalValue = products.Sum(p => p.Stock * p.Tags.Sum(t => t.Price));

            return totalValue;
        }

    }
}
