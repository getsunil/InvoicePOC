using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using UploadAndMapPoc.Models;

namespace UploadAndMapPoc.Controllers
{
    public class InvoiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("file not selected");

            var path = System.IO.Path.Combine(
                Directory.GetCurrentDirectory(), "UploadedInvoices",
                Guid.NewGuid().ToString()+"-"+file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return RedirectToAction("Files");
        }

        [HttpGet]
        public async Task<ActionResult> Files()
        {
            string folderPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "UploadedInvoices");
            UploadedFilesViewModel model = new UploadedFilesViewModel();
            model.InvoiceFiles = new List<InvoiceFile>();
            var invoicesInSystem = Directory.GetFiles(folderPath);

            foreach (string invoiceFile in invoicesInSystem)
            {
                InvoiceFile newInvoiceFile = new InvoiceFile();
                string textInvoice = ReadOrderPDFInvoice(invoiceFile);
                var invoiceLines = textInvoice.Split("\n");
                newInvoiceFile.FileName = invoiceFile;
                newInvoiceFile.InvoiceNumber = invoiceLines.Where(l => l.Contains("Invoice Numbers")).FirstOrDefault();
                newInvoiceFile.OrderIds = invoiceLines.Where(l => l.Contains("Order:")).FirstOrDefault();
                model.InvoiceFiles.Add(newInvoiceFile);
            }

            return View(model);
        }

        private string ReadOrderPDFInvoice(string filePath)
        {
            using (PdfReader reader = new PdfReader(filePath))
            {
                StringBuilder text = new StringBuilder();

                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
                }

                return text.ToString();
            }
        }

    }
}