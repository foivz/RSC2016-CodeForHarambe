using rsc_harambe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace rsc_harambe.Controllers
{
    public class AnswersTeamController : ApiController
    {
        private KvizEntities db = new KvizEntities();
        // GET: api/AnswersTeam
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/AnswersTeam/5
        public List<TeamAnswer> Get(int id)
        {
            List<TeamAnswer> teamAnswerList = new List<TeamAnswer>();

            teamAnswerList = db.TeamAnswers.Where(t => t.teamID.Equals(id)).ToList();

            return teamAnswerList;
        }

        // POST: api/AnswersTeam
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/AnswersTeam/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/AnswersTeam/5
        public void Delete(int id)
        {
        }
    }
}
