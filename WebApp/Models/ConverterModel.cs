using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class ConverterModel
    {
        public string XML { get; set; }
        public string ConvertTo { get; set; }
        public string UploadType { get; set; }
        public IFormFile FileUpload { get; set; }
    }
}
