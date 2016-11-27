using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace rsc_harambe.Controllers
{
    public class HomeController : Controller
    {
        private IFirebaseClient _client;

        public ActionResult Index()
        {

            return View();
        }

        public async Task<RedirectToRouteResult> CallFirebase()
        {
            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = "fGO6ZIeJcJyOcKohS5jsSuna3SMnaZL7qktSafix",
                BasePath = "https://rsc-harambe.firebaseio.com"
            };

            _client = new FirebaseClient(config);

            await _client.PushAsync("soba/tim1/", new
            {
                name = "someone netko",
                text = "Hello from backend :" + DateTime.Now.ToString("f")
            });

            return RedirectToAction("Index");
        }
    }
}
