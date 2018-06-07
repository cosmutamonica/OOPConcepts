using MySql.Data.MySqlClient;
using Suporteri.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Suporteri.DataAccessLayer
{
    public class ClientDAL
    {
        String connString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public void CreateClient(string userId, string name, string lastname, string latitude, string longitude, string address, string type)
        {
            using (MySqlConnection con = new MySqlConnection(connString))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "InsertClient";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters["@userId"].Direction = ParameterDirection.Input;
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters["@name"].Direction = ParameterDirection.Input;
                    cmd.Parameters.AddWithValue("@lastname", lastname);
                    cmd.Parameters["@lastname"].Direction = ParameterDirection.Input;
                    cmd.Parameters.AddWithValue("@latitude", latitude);
                    cmd.Parameters["@latitude"].Direction = ParameterDirection.Input;
                    cmd.Parameters.AddWithValue("@longitude", longitude);
                    cmd.Parameters["@longitude"].Direction = ParameterDirection.Input;
                    cmd.Parameters.AddWithValue("@address", address);
                    cmd.Parameters["@address"].Direction = ParameterDirection.Input;
                    cmd.Parameters.AddWithValue("@type", type);
                    cmd.Parameters["@type"].Direction = ParameterDirection.Input;

                    con.Open();
                    cmd.ExecuteReader();
                }
            }
        }

        public User GetClientInfo(string id)
        {
            User user = new User();
            using (MySqlConnection con = new MySqlConnection(connString))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "GetUserById";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters["@id"].Direction = ParameterDirection.Input;
                    con.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                user.FirstName = reader["name"].ToString();
                                user.LastName = reader["lastname"].ToString();
                                user.Latitude = reader["latitude"].ToString();
                                user.Longitude = reader["longitude"].ToString();
                                user.Type = reader["type"].ToString();
                                user.Address = reader["address"].ToString();
                            }
                            reader.NextResult();
                        }
                    }
                }
            }
            return user;
        }
    }
}