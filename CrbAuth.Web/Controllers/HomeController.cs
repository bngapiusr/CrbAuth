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
            http://mahedee.net/simple-notifications-with-toastr
            https://www.youtube.com/watch?v=Z8RstrIaeFA
            https://www.youtube.com/watch?v=_qIYBgWTlTo
            https://www.youtube.com/watch?v=w-OHoD47VOA
            https://www.youtube.com/watch?v=8cUlo1sfBZ4
            http://www.dotnetawesome.com/2016/12/crud-operation-using-datatables-aspnet-mvc.html
            https://www.youtube.com/watch?v=3pEax-5wXG8

            check this first
            https://www.aspforums.net/Threads/859788/Calling-JavaScript-Function-from-Controller-in-ASPNet-MVC/

            https://www.aspsnippets.com/Articles/Call-JavaScript-Function-from-Controller-in-ASPNet-MVC.aspx
            https://www.c-sharpcorner.com/forums/how-to-call-the-javascript-function-in-mvc-action-result
            https://forums.asp.net/t/1735868.aspx?Call+Javascript+Function+in+MVC+Controller+

            --------------------------------------

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
