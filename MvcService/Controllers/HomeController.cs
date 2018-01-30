using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Client;
using MvcService.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MvcService.Controllers
{
    public class HomeController : Controller
    {

        

        public HomeController()
        {
        }
        

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult About()
        {
            
            ViewData["Message"] = "Teste S2 IT.";

            return View();
        }

        public IActionResult Contact()
        {
            
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
