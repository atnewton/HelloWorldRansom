using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HelloWorldRansom.ViewModels;

namespace HelloWorldRansom.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(string translateTo)
        {
            ViewData["Host"] = System.Runtime.InteropServices.RuntimeInformation.OSDescription;
            
            if (translateTo == null) { translateTo = "en"; };
            IEnumerable<string> letters = new Ransom().GetRansomNote("HELLO WORLD", translateTo);
            
            return View(new ImagesViewModel(letters, translateTo));
        }
    }
}
