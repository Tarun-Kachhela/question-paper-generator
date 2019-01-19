using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuestionPaper
{
    class Program
    {
        static int count;
        static Questions[] questionsDetails = new Questions[100];
        static void Main(string[] args)
        {
           
           
            
            QuestionHandler questionHandler = new QuestionHandler();
            //questionHandler.Insert();
            List<int> listOfId = questionHandler.GenerateQuestion(30,3);
            for (int i = 0; i < listOfId.Count(); i++)
                Console.WriteLine("  " + listOfId.ElementAt(i));

            Console.Read();
        }
        public static void initialize(string anyQuestion,int marksOfQuestion,int randomNumber)
        {
            questionsDetails[count].question = anyQuestion;
            questionsDetails[count].marks = marksOfQuestion;
            questionsDetails[count].id = randomNumber;
            count++;
        }
    }
}
