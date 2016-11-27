using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace rsc_harambe.Models
{
    public class ParticipantCountController : ApiController
    {
        private KvizEntities db = new KvizEntities();
        // GET: api/ParticipantCount
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ParticipantCount/5
        public List<CountModel> Get(int id)
        {
            List<CountModel> cntList = new List<CountModel>();
            CountModel cnt = new CountModel();
            cnt.count = 0;

            foreach (Team team in db.Teams.Where(t => t.eventID.Equals(id)))
            {
                cnt.count++;
            }

            cntList.Add(cnt);

            return cntList;
        }

        // POST: api/ParticipantCount
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ParticipantCount/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ParticipantCount/5
        public void Delete(int id)
        {
        }
    }
}
