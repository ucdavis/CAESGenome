using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace CAESGenome.Services
{
    public interface IDbService
    {
        DbConnection GetConnection(string connectionString = null);
    }

    public class DbService : IDbService
    {
        public DbConnection GetConnection(string connectionString = null)
        {
            //If connection string is null, use the default sql ce connection
            if (connectionString == null)
            {
                connectionString = WebConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            }

            var connection = new SqlConnection(connectionString);
            connection.Open();

            return connection;
        }
    }
}