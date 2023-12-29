using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using StokManagmentSystem.Models;

namespace StokManagmentSystem.Service
{
    public class UserService
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        private readonly ILogger logger;


        public bool ValidateUserCredentials(UserModel userinfo)
        {
            bool isValid = false;

            try
            {
                MySqlConnection mcon = new MySqlConnection(connectionString);
                MySqlDataAdapter adapter;
                DataTable table = new DataTable();

                adapter = new MySqlDataAdapter("Select * From user where Username = '" + userinfo.UserName+ "' and password = '" + userinfo.Password + "'", mcon);
                adapter.Fill(table);
                if (table.Rows.Count <= 0)
                {
                    isValid = false;
                }
                else
                {
                    isValid = true;
                }
            }
            catch (Exception ex)
            {
                isValid = false;
                Console.WriteLine($"Exception: {ex.Message}");
            }

            return isValid;
        }
    }
}