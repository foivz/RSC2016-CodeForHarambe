using rsc_harambe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.Entity;
using System.Web.Mvc;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Http.Cors;

namespace rsc_harambe.Controllers
{
    public class QEventsController : ApiController
    {
        private KvizEntities db = new KvizEntities();
        // GET: api/QEvents
        public List<Event> Get()
        {
            return db.Events.ToList();
        }

        // GET: api/QEvents/5
        public List<Event> Get(int? id)
        {
            List<Event> eventList = new List<Event>();
            if (id == null)
            {
                return null;
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return null;
            }
            eventList.Add(@event);
            return eventList;
        }

        public async Task<List<Event>> Post()
        {
            string result = await Request.Content.ReadAsStringAsync();

            List<Event> eventLista = new List<Event>();

            //string result = "{ \"action\": \"create\", \"data\":[{ \"name\": \"Pobjeda Trumpa\", \"location\":\"Washington\", \"date\": \"2016 -03-06 00:00:00.000\"}]}";
            dynamic jobj = JsonConvert.DeserializeObject(result);
            string action = jobj.action;
            if (action.Equals("create"))
            {
                foreach (dynamic jdata in jobj.data)
                {
                    if (ModelState.IsValid)
                    {
                        Event @event = new Event();

                        @event.name = jdata.name;
                        @event.eDesc = jdata.eDesc;
                        @event.eDate = jdata.eDate;
                        @event.loc = jdata.loc;
                        @event.prize = jdata.prize;
                        @event.rules = jdata.rules;
                        @event.teamsize = jdata.teamsize;
                        @event.eStatus = jdata.eStatus;

                        db.Events.Add(@event);
                        db.SaveChanges();
                        eventLista.Add(@event);
                    }
                }

                return eventLista;
            }
            else if (action.Equals("update"))
            {
                foreach (dynamic jdata in jobj.data)
                {
                    Event @event = new Event();

                    @event.id = jdata.id;
                    @event.name = jdata.name;
                    @event.eDesc = jdata.eDesc;
                    @event.eDate = jdata.eDate;
                    @event.loc = jdata.loc;
                    @event.prize = jdata.prize;
                    @event.rules = jdata.rules;
                    @event.teamsize = jdata.teamsize;
                    @event.eStatus = jdata.eStatus;

                    eventLista.Add(@event);
                    db.Entry(@event).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return eventLista; // "200 OK";
            }
            else return eventLista; //"500 Internal Server Error";
        }

        // PUT: api/QEvents/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/QEvents/5
        public List<Event> Delete(int id)
        {
            List<Event> eventList = new List<Event>();
            Event @event = db.Events.Find(id);
            eventList.Add(@event);
            db.Events.Remove(@event);
            db.SaveChanges();

            return eventList;
        }
    }
}
