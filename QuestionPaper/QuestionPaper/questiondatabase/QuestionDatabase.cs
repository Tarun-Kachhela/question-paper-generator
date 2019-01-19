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
    class QuestionDatabase
    {
        MySqlConnection mySqlConnection;
        //string questions = { };
        public QuestionDatabase()
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
                mySqlConnection.Open();
                commandDatabase.ExecuteReader();
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
            var list = new List<int>();
            string sql = "SELECT * FROM questions WHERE probability =" + probability + "";
            MySqlCommand commandDatabase = new MySqlCommand(sql, mySqlConnection);
            try
            {
                mySqlConnection.Open();
                MySqlDataReader reader = commandDatabase.ExecuteReader();
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
        public string GetQuestion(int id,int marks,int chp)
        {
            string sql=null;
            int count = getCount(0);
            string question=null;
            if(count == 0)
                sql = "SELECT * FROM questions WHERE id = " + id + " AND marks ="+marks+" AND chapterno = "+chp+" AND probability = 5";
            else
                sql = "SELECT * FROM questions WHERE id = "+id + " AND marks =" + marks + " AND chapterno = " + chp + " AND probability = 0";
            MySqlCommand command = new MySqlCommand(sql, mySqlConnection);
            try
            {
                mySqlConnection.Open();
                MySqlDataReader mySqlDataReader = command.ExecuteReader();
                if (mySqlDataReader.HasRows && mySqlDataReader.Read())
                {
                    question = mySqlDataReader.GetString(1);
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

        public int  GetMarks(int id)
        {
            int markOfQuestion = 0;
            string sql = "SELECT * FROM questions WHERE id = " + id;
            MySqlCommand command = new MySqlCommand(sql, mySqlConnection);
            try
            {
                mySqlConnection.Open();
                MySqlDataReader mySqlDataReader = command.ExecuteReader();
                if (mySqlDataReader.HasRows && mySqlDataReader.Read())
                {
                    markOfQuestion = mySqlDataReader.GetInt16(3);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception from getQuestion fun : " + e.Message);
                return 0;
            }
            finally
            {
                mySqlConnection.Close();
            }
            return markOfQuestion;
        }

        public string Update(int id,int probability)
        {
            string question = null;
            string sql = "UPDATE questions SET probability =" + probability + " WHERE id ="+id+"";
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
        
        public int getCount(int condition)
        {
            int count=0;
            string sql;
            if(condition == 1)
                sql = "SELECT COUNT(probability) as total from questions";
            else
                sql = "SELECT COUNT(probability) as total from questions WHERE probability ="+condition+"";
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
