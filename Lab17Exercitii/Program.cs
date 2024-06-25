using Lab17Exercitii.Models;
using Lab17Exercitii;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

using (var context = new AppDbContext())
{
    context.Database.EnsureCreated();

    
    if (!context.Categories.Any())
    {
        context.Categories.AddRange(
            new Category { Name = "Electronics", IconUrl = "https://cdn-icons-png.flaticon.com/512/2432/2432572.png" },
            new Category { Name = "Books", IconUrl = "https://cdn-icons-png.flaticon.com/512/3429/3429149.png" },
            new Category { Name = "Food", IconUrl = "https://cdn-icons-png.flaticon.com/512/4080/4080032.png" },
            new Category { Name = "Jewelry", IconUrl = "https://cdn-icons-png.flaticon.com/512/2237/2237677.png" }
        );
        context.SaveChanges();
    }
    if (!context.Manufacturers.Any())
    {
        context.Manufacturers.AddRange(
            new Manufacturer { Name = "Sony", Address = "Tokyo, Japan", CUI = "1234" },
            new Manufacturer { Name = "Samsung", Address = "Seoul, South Korea", CUI = "4567" },
            new Manufacturer { Name = "Coca-Cola", Address = "Atlanta, Georgia, United States", CUI = "7890" },
            new Manufacturer { Name = "Tiffany", Address = " New York, New York, United States" , CUI ="1112" }
        );
        context.SaveChanges();
    }

    var productService = new ProductService(context);

    var products = new List<(string Name, int Stock, int CategoryId, int ManufacturerId, double Price)>
    {
        ("Samsung-S20", 1000, 2, 1, 149.99),
        ("Ps5", 100, 2, 1, 249.99),
        ("Cola-Zero", 10000, 3, 3, 14.99),
        ("Diamond Rings", 70, 4, 4, 62000),
        ("Silver Rings", 40,4,4,20000)
    };
    foreach (var (productName, productStock, productCategoryId, productManufacturerId, productPrice) in products)
    {
        var product = productService.AddProduct(productName, productStock, productCategoryId, productManufacturerId, Guid.NewGuid(), productPrice);
        Console.WriteLine(product);
    }
    int productIdToUpdate = 1; 
    double newPrice = 159.99; 
    productService.UpdateProductPrice(productIdToUpdate, newPrice);
    
    var totalStockValue = productService.GetTotalStockValue();
    Console.WriteLine($"Total stock value: {totalStockValue:C2}");

    int manufacturerId = 3; 
    var totalStockValueByManufacturer = productService.GetTotalStockValueByManufacturer(manufacturerId);
    Console.WriteLine($"Total stock value for Manufacturer ID {manufacturerId}: {totalStockValueByManufacturer:C2}");

    int categoryId = 1; 
    var totalStockValueByCategory = productService.GetTotalStockValueByCategory(categoryId);
    Console.WriteLine($"Total stock value for Category ID {categoryId}: {totalStockValueByCategory:C2}");


    void TotalCtgManufID(int categoryId, int manufacturerId)
    {
        var productService = new ProductService(context);
        var totalStockValueByCategoryAndManufacturer = productService.GetTotalStockValueByCategoryAndManufacturer(categoryId, manufacturerId);
        Console.WriteLine($"Total stock value for Category ID {categoryId} and Manufacturer ID {manufacturerId}: {totalStockValueByCategoryAndManufacturer:C2}");
    }
    TotalCtgManufID(4, 4);

}
        


