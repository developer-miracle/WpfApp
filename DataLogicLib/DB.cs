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
    public class DB
    {
        private string connectionString;
        private SqlConnection sqlConnection;
        public DataSet dataSet;
        private SqlDataAdapter sqlDataAdapter;
        private SqlCommandBuilder sqlCommandBuilder;

        //static DbCommand dbCommand;
        public DB()
        {
            connectionString = ConfigurationManager.ConnectionStrings["LocalConnectToDB"].ConnectionString;
            sqlConnection = new SqlConnection(connectionString);
        }
        public DataView Select(string querry)
        {
            try
            {
                dataSet = new DataSet();
                sqlDataAdapter = new SqlDataAdapter(querry, sqlConnection);
                sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);
                sqlDataAdapter.Fill(dataSet, "Books");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return dataSet.Tables["Books"].DefaultView;
        }
        public void Update()
        {
            try
            {
                sqlDataAdapter.Update(dataSet, "Books");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
