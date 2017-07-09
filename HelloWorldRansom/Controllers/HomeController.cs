using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HelloWorldRansom.ViewModels;
using Microsoft.Extensions.Configuration;

namespace HelloWorldRansom.Controllers
{
    public class HomeController : Controller
    {
        IConfiguration _config;

        public HomeController(IConfiguration config)
        {
            _config = config;
        }

        public IActionResult Index(string translateTo)
        {
            ViewData["Host"] = System.Runtime.InteropServices.RuntimeInformation.OSDescription;
            
            if (translateTo == null) { translateTo = "en"; };
            var flickrKey = _config.GetSection("Config").GetSection("FlickrApiKey").Value;
            var translateKey = _config.GetSection("Config").GetSection("MicrosoftTranslateApiKey").Value;
            IEnumerable<string> letters = new Ransom().GetRansomNote("HELLO WORLD", translateTo, flickrKey, translateKey);
            
            return View(new ImagesViewModel(letters, translateTo));
        }
    }
}
