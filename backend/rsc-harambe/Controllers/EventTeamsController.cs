using rsc_harambe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace rsc_harambe.Controllers
{
    public class EventTeamsController : ApiController
    {
        private KvizEntities db = new KvizEntities();
        // GET: api/EventTeams
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/EventTeams/5
        public List<Team> Get(int id)
        {
            List<Team> teamList = new List<Team>();

            teamList = db.Teams.Where(t => t.eventID.Equals(id)).ToList();

            return teamList;
        }

        // POST: api/EventTeams
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/EventTeams/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/EventTeams/5
        public void Delete(int id)
        {
        }
    }
}
