using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace OrderExtractorFromPDF
{
    class Program
    {
        static void Main(string[] args)
        {
            const string FILE_PATH = @"C:\Users\e10111418\Desktop\temp\Sprint\Sprint-1810-1\SP-12127\DetailInvoice (002).pdf";
            string output;
            using (PdfReader reader = new PdfReader(FILE_PATH))
            {
                StringBuilder text = new StringBuilder();

                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
                }

                output = text.ToString();
            }

            Console.WriteLine(output);
            Console.ReadKey();
        }
    }
}
