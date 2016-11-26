using rsc_harambe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace rsc_harambe.Controllers
{
    public class QuestionsQueryController : ApiController
    {
        private KvizEntities db = new KvizEntities();
        // GET: api/QuestionsQuery
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/QuestionsQuery/5
        public List<Question> Get(int id)
        {
            List<Question> questionList = new List<Question>();
            foreach(Question quest in db.Questions.ToList())
            {
                if (quest.eventID == id) questionList.Add(quest);
            }
            return questionList;
        }

        // POST: api/QuestionsQuery
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/QuestionsQuery/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/QuestionsQuery/5
        public void Delete(int id)
        {
        }
    }
}
