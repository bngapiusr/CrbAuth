using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CrbAuth.Web.Models;
using CrbAuth.Web.ViewModels;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Newtonsoft.Json;

namespace CrbAuth.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(AddUserViewModel objuserviewmodel)
        {
            /*
             * https://www.codeproject.com/Articles/1139528/Toastr-Net-Yet-Another-Notification-Plugin-Extende
             * 

        //https://johnpapa.net/toastr100beta/
        //https://www.aspforums.net/Threads/136481/Display-jQuery-Toaster-Notifications-from-Server-Side-in-ASPNet/

            ////return Json(new { success = false, message="Your request has been successfully added,."}, JsonRequestBehavior.AllowGet); ;
            ///
            */
            return View();
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
