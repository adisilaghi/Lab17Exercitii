using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab17Exercitii.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public Guid Barcode { get; set; }
        public double Price { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
