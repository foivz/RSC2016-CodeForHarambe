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
    public class UserTeamsController : ApiController
    {
        private KvizEntities db = new KvizEntities();
        // GET: api/QUserTeams
        public List<UserTeam> Get()
        {
            return db.UserTeams.ToList();
        }

        // GET: api/QUserTeams/5
        public List<UserTeam> Get(int? id)
        {
            List<UserTeam> UserTeamList = new List<UserTeam>();
            if (id == null)
            {
                return null;
            }
            UserTeam @UserTeam = db.UserTeams.Find(id);
            if (@UserTeam == null)
            {
                return null;
            }
            UserTeamList.Add(@UserTeam);
            return UserTeamList;
        }

        public async Task<List<UserTeam>> Post()
        {
            string result = await Request.Content.ReadAsStringAsync();

            List<UserTeam> UserTeamLista = new List<UserTeam>();

            //string result = "{ \"action\": \"create\", \"data\":[{ \"name\": \"Pobjeda Trumpa\", \"location\":\"Washington\", \"date\": \"2016 -03-06 00:00:00.000\"}]}";
            dynamic jobj = JsonConvert.DeserializeObject(result);
            string action = jobj.action;
            if (action.Equals("create"))
            {
                foreach (dynamic jdata in jobj.data)
                {
                    if (ModelState.IsValid)
                    {
                        UserTeam @UserTeam = new UserTeam();

                        @UserTeam.userID = jdata.userID;
                        @UserTeam.teamID = jdata.teamID;

                        db.UserTeams.Add(@UserTeam);
                        db.SaveChanges();
                        UserTeamLista.Add(@UserTeam);
                    }
                }

                return UserTeamLista;
            }
            else if (action.Equals("update"))
            {
                foreach (dynamic jdata in jobj.data)
                {
                    UserTeam @UserTeam = new UserTeam();

                    @UserTeam.id = jdata.id;
                    @UserTeam.userID = jdata.userID;
                    @UserTeam.teamID = jdata.teamID;

                    UserTeamLista.Add(@UserTeam);
                    db.Entry(@UserTeam).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return UserTeamLista; // "200 OK";
            }
            else if (action.Equals("delete"))
            {
                UserTeam @UserTeam = db.UserTeams.Find((int)jobj.id);
                UserTeamLista.Add(@UserTeam);
                db.UserTeams.Remove(@UserTeam);
                db.SaveChanges();

                return UserTeamLista;
            }
            else return UserTeamLista; //"500 Internal Server Error";
        }

        // PUT: api/QUserTeams/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/QUserTeams/5
        public List<UserTeam> Delete(int id)
        {
            List<UserTeam> UserTeamList = new List<UserTeam>();
            UserTeam @UserTeam = db.UserTeams.Find(id);
            UserTeamList.Add(@UserTeam);
            db.UserTeams.Remove(@UserTeam);
            db.SaveChanges();

            return UserTeamList;
        }
    }
}
