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
        //контейнер для хранения таблиц
        public DataSet dataSet;

        //строка подключения
        private string connectionString;
        //переменная для подключения к бд
        private SqlConnection sqlConnection;
        //адаптер для взаимодействия контейнета и бд в отсоединенном режиме
        private SqlDataAdapter sqlDataAdapter;
        //переменная для автоматизированного создания команд INSERT/UPDATE/DELETE
        private SqlCommandBuilder sqlCommandBuilder;
        //переменная для хранения SELECT запроса
        private SqlCommand selectCommand;
        //переменная для ручного создания команд INSERT/UPDATE/DELETE (делаем UPDATE вручную)
        private SqlCommand updateCommand;
        ////переменная для ручного создания команд INSERT/UPDATE/DELETE (делаем UPDATE процедуру вручную)
        private SqlCommand updateCommand2;
        private SqlCommand updateCommand3;
        //создаем коллекцию для хранения параметров
        private SqlParameterCollection sqlParameter;
        private SqlParameterCollection sqlParameter3;
        //строка, которую ввел пользователь
        private string stringAuthor;

        private string SELECTstrAuthor = @"SELECT Books.Id, Books.[name], Books.price, Authors.[name] author, Categories.[name] category FROM Books JOIN Authors ON Books.Author_id = Authors.Id JOIN Categories ON Books.category_id = Categories.Id WHERE Authors.[name] = @pName";

        //SqlParameter parameter;
        public DB()
        {
            //считываем строку подключения из AppConfig
            connectionString = ConfigurationManager.ConnectionStrings["LocalConnectToDB"].ConnectionString;
            //создаем объект подключения Sql и инициализируем строкой подключения к бд
            sqlConnection = new SqlConnection(connectionString);
            

            //---------------SELECT запрос-----(все книги по автору)-------------------------
            //1
            //parameter = new SqlParameter();
            //parameter.ParameterName = "@pName";
            //parameter.SqlDbType = SqlDbType.NVarChar;
            //parameter.Value = stringAuthor;
            //selectCommand.Parameters.Add(parameter);
            //2
            //selectCommand.Parameters.Add("@pName", SqlDbType.NVarChar).Value = stringAuthor;
            //sqlDataAdapter.SelectCommand = selectCommand;
            //selectCommand.Parameters.Add("@pAuthor_id", SqlDbType.Int).Value = Int32.Parse(SELECTstringAuthor_id));//лучше использовать TryParse(),или не использовать текстбокс вовсе, а значение предоставить на выбор строго из int-овых.
            //selectCommand.Parameters["@pAuthor_id"].SourceColumn = "Author_id";
            //--------------------------------------------------------------------------------

            //---------------создание команды UPDATE с помощью SqlCommand (+защита Sql inject)(только для изменения цены)
            updateCommand = new SqlCommand("UPDATE Books SET price = @pPrice WHERE id = @pId", sqlConnection);

            updateCommand.Parameters.Add(new SqlParameter("@pPrice", SqlDbType.Money));
            updateCommand.Parameters["@pPrice"].SourceVersion = DataRowVersion.Current;
            updateCommand.Parameters["@pPrice"].SourceColumn = "price";

            updateCommand.Parameters.Add(new SqlParameter("@pId", SqlDbType.Int));
            updateCommand.Parameters["@pId"].SourceVersion = DataRowVersion.Original;
            updateCommand.Parameters["@pId"].SourceColumn = "id";

            //заменяем команду UPDATE на созданую вручную(+защита Sql inject)
            //sqlDataAdapter.UpdateCommand = updateCommand;
            //--------------------------------------------------------------------------------

            //---------------хранение процедуры----------(работает для изменения всего)-------
            updateCommand2 = new SqlCommand("UpdateBooks", sqlConnection);
            //указываем тип комманды
            updateCommand2.CommandType = CommandType.StoredProcedure;
            
            //связываем с SqlCommand
            sqlParameter = updateCommand2.Parameters;

            sqlParameter.Add("@pId", SqlDbType.Int, 8, "id");
            sqlParameter["@pId"].SourceVersion = DataRowVersion.Original;
            sqlParameter.Add("@pName", SqlDbType.NVarChar, 200, "name");
            sqlParameter.Add("@pPrice", SqlDbType.Money, 8, "price");
            sqlParameter.Add("@pAuthorId", SqlDbType.Int, 8, "author_id");
            sqlParameter.Add("@pCategoryId", SqlDbType.Int, 8, "category_id");


            updateCommand3 = new SqlCommand("UpdateBooksAuthorsCategories", sqlConnection);
            //указываем тип комманды
            updateCommand3.CommandType = CommandType.StoredProcedure;

            //связываем с SqlCommand
            sqlParameter3 = updateCommand3.Parameters;

            sqlParameter3.Add("@pId", SqlDbType.Int, 8, "id");
            sqlParameter3["@pId"].SourceVersion = DataRowVersion.Original;
            sqlParameter3.Add("@pNameBook", SqlDbType.NVarChar, 200, "name");
            sqlParameter3.Add("@pPrice", SqlDbType.Money, 8, "price");
            sqlParameter3.Add("@pNameAuthor", SqlDbType.NVarChar, 200, "author");
            sqlParameter3.Add("@pNameCategory", SqlDbType.NVarChar, 200, "category");




            sqlDataAdapter = new SqlDataAdapter();
            
            //используем процедуру для UPDATE
            //sqlDataAdapter.UpdateCommand = updateCommand2;
            //--------------------------------------------------------------------------------
        }

        public DataView Select(string querry)
        {
            stringAuthor = querry;
            //получаем SELECT напрямую из строки запроса от пользователя(ОПАСНО) 
            //sqlDataAdapter.SelectCommand = new SqlCommand(querry, sqlConnection);
            //sqlDataAdapter = new SqlDataAdapter(SELECTstrAuthor, sqlConnection);
            selectCommand = new SqlCommand(SELECTstrAuthor, sqlConnection);
            selectCommand.Parameters.Add("@pName", SqlDbType.NVarChar).Value = stringAuthor;
            selectCommand.Parameters["@pName"].Size = 200;
            sqlDataAdapter.SelectCommand = selectCommand;

            
            try
            {
                //инициализируем контейнер для хранение таблиц
                dataSet = new DataSet();

                //инициализируем адаптер запросом SELECT и готовым подключением к бд


                //автоматически добавляем команды INSERT/UPDATE/DELETE
                sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);
                

                //заполняем DataSet из данными бд
                sqlDataAdapter.Fill(dataSet, "Books");//метод автоматически разрывает соединение с бд
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
            //возврат результата
            return dataSet.Tables["Books"].DefaultView;
        }
        
        public void Update()
        {
            try
            {
                //запись обновленных данных из контейнера в бд
                sqlDataAdapter.UpdateCommand = updateCommand3;
                sqlDataAdapter.Update(dataSet, "Books");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
