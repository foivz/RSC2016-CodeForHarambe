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
    public class QuizController : ApiController
    {
        private KvizEntities db = new KvizEntities();
        private IFirebaseClient _client;
        // GET: api/Quiz
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Quiz/5
        public string Get(int id)
        {
            //dohvati event
            return " ";
        }

        // POST: api/Quiz
        public async Task<bool> Post()
        {
            string result = await Request.Content.ReadAsStringAsync();

            List<Event> eventLista = new List<Event>();

            //string result = "{ \"action\": \"create\", \"data\":[{ \"name\": \"Pobjeda Trumpa\", \"location\":\"Washington\", \"date\": \"2016 -03-06 00:00:00.000\"}]}";
            dynamic jobj = JsonConvert.DeserializeObject(result);
            int id = (int)jobj.id;
            int pitanje = (int)jobj.pitanje;

            if (id == null)
            {
                return false;
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return false;
            }

            string path = "quizFlow/" + id;
            string fName = "" + pitanje;
            string fText = "";

            if (pitanje > 0)
            {
                foreach(Answer answer in db.Answers.Where(a => a.questionID.Equals(pitanje)))
                {
                    fText += answer.id + ";";
                }
            }

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

        // PUT: api/Quiz/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Quiz/5
        public void Delete(int id)
        {
        }
    }
}
