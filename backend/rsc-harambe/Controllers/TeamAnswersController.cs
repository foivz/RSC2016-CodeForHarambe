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
using FireSharp.Interfaces;
using FireSharp;
using FireSharp.Config;

namespace rsc_harambe.Controllers
{
    public class TeamAnswersController : ApiController
    {
        private KvizEntities db = new KvizEntities();
        private IFirebaseClient _client;
        // GET: api/QTeamAnswers
        public List<TeamAnswer> Get()
        {
            return db.TeamAnswers.ToList();
        }

        // GET: api/QTeamAnswers/5
        public List<TeamAnswer> Get(int? id)
        {
            List<TeamAnswer> TeamAnswerList = new List<TeamAnswer>();
            if (id == null)
            {
                return null;
            }
            TeamAnswer @TeamAnswer = db.TeamAnswers.Find(id);
            if (@TeamAnswer == null)
            {
                return null;
            }
            TeamAnswerList.Add(@TeamAnswer);
            return TeamAnswerList;
        }

        public async Task<List<TeamAnswer>> Post()
        {
            string result = await Request.Content.ReadAsStringAsync();

            List<TeamAnswer> TeamAnswerLista = new List<TeamAnswer>();

            //string result = "{ \"action\": \"create\", \"data\":[{ \"name\": \"Pobjeda Trumpa\", \"location\":\"Washington\", \"date\": \"2016 -03-06 00:00:00.000\"}]}";
            dynamic jobj = JsonConvert.DeserializeObject(result);
            string action = jobj.action;
            if (action.Equals("create"))
            {
                foreach (dynamic jdata in jobj.data)
                {
                    if (ModelState.IsValid)
                    {
                        TeamAnswer @TeamAnswer = new TeamAnswer();

                        @TeamAnswer.answerID = jdata.answerID;
                        @TeamAnswer.teamID = jdata.teamID;
                        @TeamAnswer.eventID = jdata.eventID;
                        @TeamAnswer.points = jdata.points;
                        @TeamAnswer.answersText = jdata.answersText;

                        db.TeamAnswers.Add(@TeamAnswer);
                        db.SaveChanges();
                        TeamAnswerLista.Add(@TeamAnswer);

                        string path = "room/" + jdata.teamid;
                        string fName = jdata.teamid;
                        string fText = jdata.answersText;

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
                }

                return TeamAnswerLista;
            }
            else if (action.Equals("update"))
            {
                foreach (dynamic jdata in jobj.data)
                {
                    TeamAnswer @TeamAnswer = new TeamAnswer();

                    @TeamAnswer.id = jdata.id;
                    @TeamAnswer.answerID = jdata.answerID;
                    @TeamAnswer.teamID = jdata.teamID;
                    @TeamAnswer.eventID = jdata.eventID;
                    @TeamAnswer.points = jdata.points;
                    @TeamAnswer.answersText = jdata.answersText;

                    TeamAnswerLista.Add(@TeamAnswer);
                    db.Entry(@TeamAnswer).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return TeamAnswerLista; // "200 OK";
            }
            else return TeamAnswerLista; //"500 Internal Server Error";
        }

        // PUT: api/QTeamAnswers/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/QTeamAnswers/5
        public List<TeamAnswer> Delete(int id)
        {
            List<TeamAnswer> TeamAnswerList = new List<TeamAnswer>();
            TeamAnswer @TeamAnswer = db.TeamAnswers.Find(id);
            TeamAnswerList.Add(@TeamAnswer);
            db.TeamAnswers.Remove(@TeamAnswer);
            db.SaveChanges();

            return TeamAnswerList;
        }
    }
}
