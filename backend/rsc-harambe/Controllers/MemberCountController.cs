using rsc_harambe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace rsc_harambe.Controllers
{
    public class MemberCountController : ApiController
    {
        private KvizEntities db = new KvizEntities();
        // GET: api/MemberCount
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/MemberCount/5
        public List<CountModel> Get(int id)
        {
            List<CountModel> cntList = new List<CountModel>();
            CountModel cnt = new CountModel();
            cnt.count = 0;

            foreach(UserTeam ut in db.UserTeams.Where(u => u.teamID.Equals(id)))
            {
                cnt.count++;
            }

            cntList.Add(cnt);

            return cntList;
        }

        // POST: api/MemberCount
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/MemberCount/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/MemberCount/5
        public void Delete(int id)
        {
        }
    }
}
