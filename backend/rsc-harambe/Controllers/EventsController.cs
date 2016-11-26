using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using rsc_harambe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;

namespace rsc_harambe.Controllers
{
    public class EventsController : ApiController
    {

        // GET api/events
        /// <summary>
        /// Vraća JSON array sa svim unosima
        /// </summary>
        public List<EventsModel> Get()
        {
            EventsModel eventovi = new EventsModel();
            return eventovi.EventSelect();
        }

        // GET api/events/5
        public List<EventsModel> Get(int id)
        {
            EventsModel eventovi = new EventsModel();
            return eventovi.EventSelectByID(id);
        }

        public async Task<List<EventsModel>> Post()
        {
            string result = await Request.Content.ReadAsStringAsync();

            EventsModel eventovi = new EventsModel();
            List<EventsModel> elista = new List<EventsModel>();

            //string result = "{ \"action\": \"create\", \"data\":[{ \"name\": \"Pobjeda Trumpa\", \"location\":\"Washington\", \"date\": \"2016 -03-06 00:00:00.000\"}]}";
            dynamic jobj = JsonConvert.DeserializeObject(result);
            string action = jobj.action;
            if (action.Equals("create"))
            {
                foreach (dynamic jdata in jobj.data)
                {
                    eventovi = eventovi.EventInsert(jdata);
                    elista.Add(eventovi);
                }

                return elista;
            }
            else if (action.Equals("update"))
            {
                foreach (dynamic jdata in jobj.data)
                {
                    eventovi = eventovi.EventUpdate(jdata);
                    elista.Add(eventovi);
                }

                return elista; // "200 OK";
            }
            else return elista; //"500 Internal Server Error";
            //{ 'Name': 'Pohod na UK', 'Location': 'London', 'Date': '2017-02-05 00:00:00.000', data: []}
        }

        public string Delete(int id)
        {
            EventsModel eventovi = new EventsModel();
            bool check = eventovi.EventDelete(id);
            if(check) return "200 OK";
            else return "501 Not Implemented";
        }

    }
}
