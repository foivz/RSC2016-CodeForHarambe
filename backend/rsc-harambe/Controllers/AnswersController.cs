﻿using rsc_harambe.Models;
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
    public class AnswersController : ApiController
    {
        private KvizEntities db = new KvizEntities();
        // GET: api/QAnswers
        public List<Answer> Get()
        {
            return db.Answers.ToList();
        }

        // GET: api/QAnswers/5
        public List<Answer> Get(int? id)
        {
            List<Answer> AnswerList = new List<Answer>();
            if (id == null)
            {
                return null;
            }
            Answer @Answer = db.Answers.Find(id);
            if (@Answer == null)
            {
                return null;
            }
            AnswerList.Add(@Answer);
            return AnswerList;
        }

        public async Task<List<Answer>> Post()
        {
            string result = await Request.Content.ReadAsStringAsync();

            List<Answer> AnswerLista = new List<Answer>();

            //string result = "{ \"action\": \"create\", \"data\":[{ \"name\": \"Pobjeda Trumpa\", \"location\":\"Washington\", \"date\": \"2016 -03-06 00:00:00.000\"}]}";
            dynamic jobj = JsonConvert.DeserializeObject(result);
            string action = jobj.action;
            if (action.Equals("create"))
            {
                foreach (dynamic jdata in jobj.data)
                {
                    if (ModelState.IsValid)
                    {
                        Answer @Answer = new Answer();

                        @Answer.questionID = jdata.questionID;
                        @Answer.aText = jdata.aText;
                        @Answer.isCorrect = jdata.isCorrect;

                        db.Answers.Add(@Answer);
                        db.SaveChanges();
                        AnswerLista.Add(@Answer);
                    }
                }

                return AnswerLista;
            }
            else if (action.Equals("update"))
            {
                foreach (dynamic jdata in jobj.data)
                {
                    Answer @Answer = new Answer();

                    @Answer.id = jdata.id;
                    @Answer.questionID = jdata.questionID;
                    @Answer.aText = jdata.aText;
                    @Answer.isCorrect = jdata.isCorrect;

                    AnswerLista.Add(@Answer);
                    db.Entry(@Answer).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return AnswerLista; // "200 OK";
            }
            else if (action.Equals("delete"))
            {
                Answer @Answer = db.Answers.Find((int)jobj.id);
                AnswerLista.Add(@Answer);
                db.Answers.Remove(@Answer);
                db.SaveChanges();

                return AnswerLista;
            }
            else return AnswerLista; //"500 Internal Server Error";
        }

        // PUT: api/QAnswers/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/QAnswers/5
        public List<Answer> Delete(int id)
        {
            List<Answer> AnswerList = new List<Answer>();
            Answer @Answer = db.Answers.Find(id);
            AnswerList.Add(@Answer);
            db.Answers.Remove(@Answer);
            db.SaveChanges();

            return AnswerList;
        }
    }
}
