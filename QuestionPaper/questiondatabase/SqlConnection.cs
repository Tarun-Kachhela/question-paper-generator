using System;
using MySql.Data.MySqlClient;
namespace QuestionPaper
{
    class SqlConnection
    {
        public static MySqlConnection ConnectDb(string DatabaseName)
        {
            String MySqlConnectionString = "datasource=127.0.0.1;port=3306;username=tarun;password=tarun123;database=" + DatabaseName + ";SSL Mode=none";
            return new MySqlConnection(MySqlConnectionString);
        }
    }
}
