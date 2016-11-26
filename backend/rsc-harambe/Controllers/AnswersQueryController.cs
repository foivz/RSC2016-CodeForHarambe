using rsc_harambe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace rsc_harambe.Controllers
{
    public class AnswersQueryController : ApiController
    {
        private KvizEntities db = new KvizEntities();
        // GET: api/AnswersQuery
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/AnswersQuery/5
        public List<Answer> Get(int id)
        {
            List<Answer> answerList = new List<Answer>();
            foreach (Answer ans in db.Answers.ToList())
            {
                if (ans.questionID == id) answerList.Add(ans);
            }
            return answerList;
        }

        // POST: api/AnswersQuery
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/AnswersQuery/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/AnswersQuery/5
        public void Delete(int id)
        {
        }
    }
}
