using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Windows.Input;
using System.Windows.Forms;

namespace QuestionPaper
{
    class Question
    {
        MySqlConnection mySqlConnection;
        SqlConnection sqlConnection;
        public Question()
        {
            mySqlConnection = SqlConnection.ConnectDb("questiongenerator");
        }
        public void Insert(string question, int chapterno, int marks)
        {
            string sql = "INSERT INTO questions(id, question, chapterno, marks, probability) VALUES (NULL,'" + question + "','" + chapterno + "','" + marks + "',0)";
            MySqlCommand commandDatabase = new MySqlCommand(sql, mySqlConnection);
            commandDatabase.CommandTimeout = 60;
            try
            {
                for (int i = 0; i < 100; i++)
                {
                    mySqlConnection.Open();
                    commandDatabase.ExecuteReader();
                    mySqlConnection.Close(); 
                }
                Console.WriteLine("Data is inserted : ");
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception : " + e.Message);
            }
            finally
            {
                mySqlConnection.Close();
            }
        }
        public List<int> GetProbabilityCount(int probability=0)
        {
            int count;
            var list = new List<int>();
            string sql = "SELECT * FROM questions WHERE probability =" + probability + "";
            MySqlCommand commandDatabase = new MySqlCommand(sql, mySqlConnection);
            try
            {
                mySqlConnection.Open();
                MySqlDataReader reader = commandDatabase.ExecuteReader();
                //MessageBox.Show("Your Query Result generated ,Please See the Console : ");
                count = 0;
                while (reader.HasRows && reader.Read())
                    list.Add(reader.GetInt16(0));
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception from select fun : " + e.Message);
                return null;
            }
            finally
            {
                mySqlConnection.Close();
            }
            return list;
        }
        public string GetQuestion(int id)
        {
            string question=null;
            string sql = "SELECT * FROM questions WHERE id = "+id+ " AND probability = 0";
            MySqlCommand command = new MySqlCommand(sql, mySqlConnection);
            try
            {
                mySqlConnection.Open();
                MySqlDataReader mySqlDataReader = command.ExecuteReader();
                while (mySqlDataReader.HasRows && mySqlDataReader.Read())
                {
                    question =mySqlDataReader.GetString(1);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception from getQuestion fun : " + e.Message);
                return null;
            }
            finally
            {
                mySqlConnection.Close();
            }
            return question;
        }
        

        public string Update(int id,int probability)
        {
            string question = null;
            string sql = "UPDATE questions SET probability ='" + probability + "' WHERE id ="+id+"";
            MySqlCommand command = new MySqlCommand(sql, mySqlConnection);
            try
            {
                mySqlConnection.Open();
                command.ExecuteReader();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception from getQuestion fun : " + e.Message);
                return null;
            }
            finally
            {
                mySqlConnection.Close();
            }
            return question;
        }
        
        public int getCount()
        {
             int count=0;
             string sql = "SELECT COUNT(probability) as total from questions";
            MySqlCommand command = new MySqlCommand(sql, mySqlConnection);
            try
            {
                mySqlConnection.Open();
                MySqlDataReader mySqlDataReader = command.ExecuteReader();
                while (mySqlDataReader.HasRows && mySqlDataReader.Read())
                {
                    count = mySqlDataReader.GetInt16("total");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception from getcount fun : " + e.Message);
                return 0;
            }
            finally
            {
                mySqlConnection.Close();
            }
            return count;
        }
    }
}
