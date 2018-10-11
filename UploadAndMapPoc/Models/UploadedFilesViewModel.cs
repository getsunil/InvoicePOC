using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UploadAndMapPoc.Models
{
    public class UploadedFilesViewModel
    {
        public List<InvoiceFile> InvoiceFiles { get; set; }
    }

    public class InvoiceFile
    {
        public string FileName { get; set; }
        public string InvoiceNumber { get; set; }
        public string OrderIds { get; set; }
    }
}
