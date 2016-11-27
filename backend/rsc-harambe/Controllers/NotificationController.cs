using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using Newtonsoft.Json;
using rsc_harambe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace rsc_harambe.Controllers
{
    public class NotificationController : ApiController
    {
        private KvizEntities db = new KvizEntities();
        private IFirebaseClient _client;
        // GET: api/Notification
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Notification/5
        public async Task<bool> Get(int? id)
        {
            //dohvati event
            if (id == null)
            {
                return false;
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return false;
            }

            string path = "newEvents/";
            string fName = "admin";
            string fText = "Event " + @event.name + " is starting soon!";

                IFirebaseConfig config = new FirebaseConfig
                {
                    AuthSecret = "fGO6ZIeJcJyOcKohS5jsSuna3SMnaZL7qktSafix",
                    BasePath = "https://rsc-harambe.firebaseio.com"
                };

            _client = new FirebaseClient(config);

            await _client.PushAsync(path, new
            {
                name = fName,
                text = fText
            });

            return true; // "200 OK";
        }

        // POST: api/Notification
        public async Task<bool> Post()
        {
            string result = await Request.Content.ReadAsStringAsync();

            return true;
            }

        // PUT: api/Notification/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Notification/5
        public void Delete(int id)
        {
        }
    }
}
