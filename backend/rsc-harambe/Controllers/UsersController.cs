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
    public class UsersController : ApiController
    {
        private QuissEntities db = new QuissEntities();
        // GET: api/QUsers
        public List<User> Get()
        {
            return db.Users.ToList();
        }

        // GET: api/QUsers/5
        public List<User> Get(int? id)
        {
            List<User> UserList = new List<User>();
            if (id == null)
            {
                return null;
            }
            User @User = db.Users.Find(id);
            if (@User == null)
            {
                return null;
            }
            UserList.Add(@User);
            return UserList;
        }

        public async Task<List<User>> Post()
        {
            string result = await Request.Content.ReadAsStringAsync();

            List<User> UserLista = new List<User>();

            //string result = "{ \"action\": \"create\", \"data\":[{ \"name\": \"Pobjeda Trumpa\", \"location\":\"Washington\", \"date\": \"2016 -03-06 00:00:00.000\"}]}";
            dynamic jobj = JsonConvert.DeserializeObject(result);
            string action = jobj.action;
            if (action.Equals("create"))
            {
                foreach (dynamic jdata in jobj.data)
                {
                    if (ModelState.IsValid)
                    {
                        User @User = new User();

                        @User.name = jdata.name;
                        @User.token = jdata.token;

                        db.Users.Add(@User);
                        db.SaveChanges();
                        UserLista.Add(@User);
                    }
                }

                return UserLista;
            }
            else if (action.Equals("update"))
            {
                foreach (dynamic jdata in jobj.data)
                {
                    User @User = new User();

                    @User.id = jdata.id;
                    @User.name = jdata.name;
                    @User.token = jdata.token;

                    UserLista.Add(@User);
                    db.Entry(@User).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return UserLista; // "200 OK";
            }
            else return UserLista; //"500 Internal Server Error";
        }

        // PUT: api/QUsers/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/QUsers/5
        public List<User> Delete(int id)
        {
            List<User> UserList = new List<User>();
            User @User = db.Users.Find(id);
            UserList.Add(@User);
            db.Users.Remove(@User);
            db.SaveChanges();

            return UserList;
        }
    }
}
