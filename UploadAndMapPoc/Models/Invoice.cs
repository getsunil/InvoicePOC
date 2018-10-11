using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UploadAndMapPoc.Models
{
    public class Invoice
    {
        public string Name { get; set; }
        public string InvoiceNumber { get; set; }
        public IEnumerable<string> Orders { get; set; }
    }
}
