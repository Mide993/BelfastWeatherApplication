using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApplication.Models
{
    public class Source
    {
        public string title { get; set; }
        public string slug { get; set; }
        public string url { get; set; }
        public int crawl_rate { get; set; }
    }
}
