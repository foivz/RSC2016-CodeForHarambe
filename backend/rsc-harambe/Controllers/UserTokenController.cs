using rsc_harambe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace rsc_harambe.Controllers
{
    public class UserTokenController : ApiController
    {
        private KvizEntities db = new KvizEntities();
        // GET: api/UserToken
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/UserToken/5
        public List<User> Get(string id)
        {
            List<User> userList = new List<User>();

            userList = db.Users.Where(u => u.token.Equals(id)).ToList();

            return userList;
        }

        // POST: api/UserToken
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/UserToken/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/UserToken/5
        public void Delete(int id)
        {
        }
    }
}
