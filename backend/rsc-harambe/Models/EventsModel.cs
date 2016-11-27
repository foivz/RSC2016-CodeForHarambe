using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace rsc_harambe.Models
{
    public class EventsModel
    {
        public int id;
        public string name { get; set; }
        public string location { get; set; }
        public DateTime date { get; set; }

        public EventsModel EventInsert(dynamic jobj)
        {
            EventsModel events = new EventsModel();
            events.name = jobj.name;
            events.location = jobj.location;
            events.date = jobj.date;

            SqlConnection conn = null;
            SqlDataReader reader = null;

            try
            {
                conn = new
                    SqlConnection("Data Source=rsc-harambe.database.windows.net;Initial Catalog=rsc-harambe;Integrated Security=False;User ID=harambe;Password=Ismynigga123!;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                conn.Open();

                // 1. declare command object with parameter
                SqlCommand cmd = new SqlCommand(
                    "insert into Eventovi(EventName, LocationName, EventTime) values (@name, @location, @time);SELECT CAST(scope_identity() AS int)", conn);

                // 2. define parameters used in command object
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter();
                param[1] = new SqlParameter();
                param[2] = new SqlParameter();

                param[0].ParameterName = "@name";
                param[0].Value = events.name;
                param[1].ParameterName = "@location";
                param[1].Value = events.location;
                param[2].ParameterName = "@time";
                param[2].Value = events.date;

                // 3. add new parameter to command object
                cmd.Parameters.AddRange(param);

                // get data stream
                //cmd.ExecuteNonQuery();
                events.id = (int)cmd.ExecuteScalar();
            }
            finally
            {
                // close reader
                if (reader != null)
                {
                    reader.Close();
                }

                // close connection
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return events;
        }

        public EventsModel EventUpdate(dynamic jobj)
        {
            EventsModel events = new EventsModel();
            events.id = jobj.id;
            events.name = jobj.name;
            events.location = jobj.location;
            events.date = jobj.date;

            SqlConnection conn = null;
            SqlDataReader reader = null;

            try
            {
                conn = new
                    SqlConnection("Data Source=rsc-harambe.database.windows.net;Initial Catalog=rsc-harambe;Integrated Security=False;User ID=harambe;Password=Ismynigga123!;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                conn.Open();

                // 1. declare command object with parameter
                SqlCommand cmd = new SqlCommand(
                    "UPDATE Eventovi SET EventName = @name, LocationName = @location, EventTime = @time WHERE EventID = @eid", conn);

                // 2. define parameters used in command object
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter();
                param[1] = new SqlParameter();
                param[2] = new SqlParameter();
                param[3] = new SqlParameter();

                param[0].ParameterName = "@name";
                param[0].Value = events.name;
                param[1].ParameterName = "@location";
                param[1].Value = events.location;
                param[2].ParameterName = "@time";
                param[2].Value = events.date;
                param[3].ParameterName = "@eid";
                param[3].Value = events.id;

                // 3. add new parameter to command object
                cmd.Parameters.AddRange(param);

                // get data stream
                cmd.ExecuteNonQuery();
            }
            finally
            {
                // close reader
                if (reader != null)
                {
                    reader.Close();
                }

                // close connection
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return events;
        }

        public List<EventsModel> EventSelect()
        {
            SqlConnection conn = null;
            SqlDataReader reader = null;

            List<EventsModel> eventovi = new List<EventsModel>();

            try
            {
                conn = new
                    SqlConnection("Data Source=rsc-harambe.database.windows.net;Initial Catalog=rsc-harambe;Integrated Security=False;User ID=harambe;Password=Ismynigga123!;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    "select * from Eventovi", conn);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    EventsModel temp = new EventsModel();
                    temp.id = Int32.Parse(reader["EventID"].ToString());
                    temp.name = reader["EventName"].ToString();
                    temp.location = reader["LocationName"].ToString();
                    temp.date = (DateTime)reader["EventTime"];

                    eventovi.Add(temp);
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

                if (conn != null)
                {
                    conn.Close();
                }
            }

            return eventovi;
        }

        public List<EventsModel> EventSelectByID(int eid)
        {
            SqlConnection conn = null;
            SqlDataReader reader = null;

            List<EventsModel> eventovi = new List<EventsModel>();

            try
            {
                conn = new
                    SqlConnection("Data Source=rsc-harambe.database.windows.net;Initial Catalog=rsc-harambe;Integrated Security=False;User ID=harambe;Password=Ismynigga123!;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    "select * from Eventovi where EventID=@eid", conn);

                cmd.Parameters.AddWithValue("@eid", eid.ToString());

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    EventsModel temp = new EventsModel();
                    temp.id = Int32.Parse(reader["EventID"].ToString());
                    temp.name = reader["EventName"].ToString();
                    temp.location = reader["LocationName"].ToString();
                    temp.date = (DateTime)reader["EventTime"];

                    eventovi.Add(temp);
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

                if (conn != null)
                {
                    conn.Close();
                }
            }

            return eventovi;
        }

        public bool EventDelete(int oid)
        {
            SqlConnection conn = null;
            SqlDataReader reader = null;

            List<EventsModel> eventovi = new List<EventsModel>();

            try
            {
                conn = new
                    SqlConnection("Data Source=rsc-harambe.database.windows.net;Initial Catalog=rsc-harambe;Integrated Security=False;User ID=harambe;Password=Ismynigga123!;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                conn.Open();

                SqlCommand cmd = new SqlCommand("DELETE FROM Eventovi WHERE EventID = @uid", conn);

                cmd.Parameters.AddWithValue("@uid", oid.ToString());

                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

                if (conn != null)
                {
                    conn.Close();
                }
            }

            return true;
        }
    }
}