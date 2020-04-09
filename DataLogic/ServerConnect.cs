using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace DataLogic
{
    public static class ServerConnect
    {
        static string connectionString;
        //static SqlConnection sqlConnection;
        static SqlDataAdapter sqlDataAdapter;
        static DataSet dataSet;
        static SqlCommandBuilder sqlCommandBuilder;

        //static DbCommand dbCommand;
        static ServerConnect()
        {
            connectionString = ConfigurationManager.ConnectionStrings["LocalConnectToDB"].ConnectionString;
        }
        public static DataView Select(string querry)
        {

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                dataSet = new DataSet();
                sqlDataAdapter = new SqlDataAdapter(querry, sqlConnection);
                sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);
                sqlDataAdapter.Fill(dataSet, "Books");
            }
            return dataSet.Tables["Books"].DefaultView;
        }
    }
}
