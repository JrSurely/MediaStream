using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class FileModel
    {
        public string FileName { get; set; }
        public string Size { get; set; }

        public string TotalSeconds { get; set; }
    }
}