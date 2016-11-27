using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace rsc_harambe.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {

            return View();
        }

    }
}
