using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab17Exercitii.Models
{
    public class Manufacturer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string CUI { get; set; }
        public List<Product> Products { get; set; }

        public Manufacturer()
        {
            Products = new List<Product>();
        }
    }
}
