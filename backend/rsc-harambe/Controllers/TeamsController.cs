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
    public class TeamsController : ApiController
    {
        private KvizEntities db = new KvizEntities();
        // GET: api/QTeams
        public List<Team> Get()
        {
            return db.Teams.ToList();
        }

        // GET: api/QTeams/5
        public List<Team> Get(int? id)
        {
            List<Team> TeamList = new List<Team>();
            if (id == null)
            {
                return null;
            }
            Team @Team = db.Teams.Find(id);
            if (@Team == null)
            {
                return null;
            }
            TeamList.Add(@Team);
            return TeamList;
        }

        public async Task<List<Team>> Post()
        {
            string result = await Request.Content.ReadAsStringAsync();

            List<Team> TeamLista = new List<Team>();

            //string result = "{ \"action\": \"create\", \"data\":[{ \"name\": \"Pobjeda Trumpa\", \"location\":\"Washington\", \"date\": \"2016 -03-06 00:00:00.000\"}]}";
            dynamic jobj = JsonConvert.DeserializeObject(result);
            string action = jobj.action;
            if (action.Equals("create"))
            {
                foreach (dynamic jdata in jobj.data)
                {
                    if (ModelState.IsValid)
                    {
                        Team @Team = new Team();

                        @Team.name = jdata.name;
                        @Team.eventID = jdata.eventid;

                        db.Teams.Add(@Team);
                        db.SaveChanges();
                        TeamLista.Add(@Team);
                    }
                }

                return TeamLista;
            }
            else if (action.Equals("update"))
            {
                foreach (dynamic jdata in jobj.data)
                {
                    Team @Team = new Team();

                    @Team.id = jdata.id;
                    @Team.name = jdata.name;
                    @Team.eventID = jdata.eventid;

                    TeamLista.Add(@Team);
                    db.Entry(@Team).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return TeamLista; // "200 OK";
            }
            else return TeamLista; //"500 Internal Server Error";
        }

        // PUT: api/QTeams/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/QTeams/5
        public List<Team> Delete(int id)
        {
            List<Team> TeamList = new List<Team>();
            Team @Team = db.Teams.Find(id);
            TeamList.Add(@Team);
            db.Teams.Remove(@Team);
            db.SaveChanges();

            return TeamList;
        }
    }
}
