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

namespace rsc_harambe.Controllers
{
    public class QuestionsController : ApiController
    {
        private KvizEntities db = new KvizEntities();
        // GET: api/QQuestions
        public List<Question> Get()
        {
            return db.Questions.ToList();
        }

        // GET: api/QQuestions/5
        public List<Question> Get(int? id)
        {
            List<Question> QuestionList = new List<Question>();
            if (id == null)
            {
                return null;
            }
            Question @Question = db.Questions.Find(id);
            if (@Question == null)
            {
                return null;
            }
            QuestionList.Add(@Question);
            return QuestionList;
        }

        public async Task<List<Question>> Post()
        {
            string result = await Request.Content.ReadAsStringAsync();

            List<Question> QuestionLista = new List<Question>();

            //string result = "{ \"action\": \"create\", \"data\":[{ \"name\": \"Pobjeda Trumpa\", \"location\":\"Washington\", \"date\": \"2016 -03-06 00:00:00.000\"}]}";
            dynamic jobj = JsonConvert.DeserializeObject(result);
            string action = jobj.action;
            if (action.Equals("create"))
            {
                foreach (dynamic jdata in jobj.data)
                {
                    if (ModelState.IsValid)
                    {
                        Question @Question = new Question();

                        @Question.qType = jdata.qType;
                        @Question.qText = jdata.qText;
                        @Question.qTime = jdata.qTime;
                        @Question.eventID = jdata.eventID;

                        db.Questions.Add(@Question);
                        db.SaveChanges();
                        QuestionLista.Add(@Question);
                    }
                }

                return QuestionLista;
            }
            else if (action.Equals("update"))
            {
                foreach (dynamic jdata in jobj.data)
                {
                    Question @Question = new Question();

                    @Question.id = jdata.id;
                    @Question.qType = jdata.qType;
                    @Question.qText = jdata.qText;
                    @Question.qTime = jdata.qTime;
                    @Question.eventID = jdata.eventID;

                    QuestionLista.Add(@Question);
                    db.Entry(@Question).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return QuestionLista; // "200 OK";
            }
            else if (action.Equals("delete"))
            {
                    Question @Question = db.Questions.Find((int)jobj.id);
                    QuestionLista.Add(@Question);
                    db.Questions.Remove(@Question);
                    db.SaveChanges();

                return QuestionLista;
            }
            else return QuestionLista; //"500 Internal Server Error";
        }

        // PUT: api/QQuestions/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/QQuestions/5
        public List<Question> Delete(int id)
        {
            List<Question> QuestionList = new List<Question>();
            Question @Question = db.Questions.Find(id);
            QuestionList.Add(@Question);
            db.Questions.Remove(@Question);
            db.SaveChanges();

            return QuestionList;
        }
    }
}
