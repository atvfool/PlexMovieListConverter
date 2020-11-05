using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PlexXMLConverter;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Converter()
        {
            return View(new ConverterModel());
        }

        public IActionResult Export(ConverterModel model)
        {
            MemoryStream ms = new MemoryStream();
            byte[] fileBytes;
            XMLConverter converter = new XMLConverter(model.XML);

            string fileName = "export";
            string mimeType = "text/plain";

            FileContentResult result;
            using (StreamWriter sw = new StreamWriter(ms))
            {
                if (model.ConvertTo == "CSV")
                {
                    sw.Write(converter.GetCSV());
                    fileName = "export.csv";
                    mimeType = "text/csv";
                }
                else
                {
                    sw.Write(converter.GetJson());
                    fileName = "export.json";
                    mimeType = "application/json";
                }
                
                fileBytes = ms.GetBuffer();
            }

            result = File(fileBytes, mimeType, fileName);
            
            return result;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
