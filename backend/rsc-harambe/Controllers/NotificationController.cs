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
        private IFirebaseClient _client;
        // GET: api/Notification
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Notification/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Notification
        public async Task<List<Event>> Post()
        {
            string result = await Request.Content.ReadAsStringAsync();

            List<Event> notificationList = new List<Event>();

            dynamic jobj = JsonConvert.DeserializeObject(result);
            string action = jobj.action;

                foreach (dynamic jdata in jobj.data)
                {
                    Event @event = new Event();

                @event.name = jdata.name;
                @event.eDesc = jdata.edesc;
                @event.eDate = jdata.edate;
                @event.loc = jdata.loc;
                @event.prize = jdata.prize;
                @event.rules = jdata.rules;
                @event.teamsize = jdata.teamsize;
                @event.eStatus = jdata.estatus;

                notificationList.Add(@event);

                string path = "newEvents/";
                string fName = "admin";
                string fText = JsonConvert.SerializeObject(@event);

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
            }
                return notificationList; // "200 OK";
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
