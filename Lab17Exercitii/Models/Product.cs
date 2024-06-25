using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;

namespace Lab17Exercitii.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<Tag> Tags { get; set; }

        public Product()
        {
            Tags = new List<Tag>();
        }
    }
}
