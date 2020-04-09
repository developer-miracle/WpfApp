using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogicLib
{
    public static class DB
    {
        static string connectionString;
        //static SqlConnection sqlConnection;
        static SqlDataAdapter sqlDataAdapter;
        static DataSet dataSet;
        static SqlCommandBuilder sqlCommandBuilder;

        //static DbCommand dbCommand;
        static DB()
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
