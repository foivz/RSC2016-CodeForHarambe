using rsc_harambe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace rsc_harambe.Controllers
{
    public class LeaderboardController : ApiController
    {
        private KvizEntities db = new KvizEntities();
        // GET: api/Leaderboard
        public List<LeaderboardModel> Get()
        {
            List<LeaderboardModel> lbm = new List<LeaderboardModel>();

            foreach(User user in db.Users.ToList())
            {
                LeaderboardModel userBoard = new LeaderboardModel();
                userBoard.name = user.name;
                userBoard.totalPoints = 0;
                userBoard.gameCount = 0;

                foreach(UserTeam ut in db.UserTeams.Where(u => u.userID.Equals(user.id)))
                {
                    foreach (TeamAnswer ta in db.TeamAnswers.Where(d => d.teamID.Equals(ut.teamID)))
                    {
                        if(ta.points != null) userBoard.totalPoints += (int)ta.points;
                    }
                    userBoard.gameCount++;
                }

                lbm.Add(userBoard);
            }

            return lbm;
        }

        // GET: api/Leaderboard/5
        public List<Event> Get(int id)
        {
            List<Event> eventList = new List<Event>();

            foreach(UserTeam ut in db.UserTeams.Where(u => u.userID.Equals(id)))
            {
                Team team = db.Teams.Find(ut.teamID);
                eventList.Add(db.Events.Find(team.eventID));
            }

            return eventList;
        }

        // POST: api/Leaderboard
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Leaderboard/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Leaderboard/5
        public void Delete(int id)
        {
        }
    }
}
