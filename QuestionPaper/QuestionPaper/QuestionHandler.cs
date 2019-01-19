using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionPaper
{
    class QuestionHandler
    {
        /// <summary>
        /// variables
        /// </summary>
        private Random random;
        private QuestionDatabase question;

        public QuestionHandler()
        {
            random = new Random();
            question = new QuestionDatabase();

        }
        private void UpdateProbabilityAsFive()
        {
            List<int> list = question.GetProbabilityCount(10);
            for (int i = 0; i < list.Count(); i++)
                question.Update(list.ElementAt(i), 5);
        }
        public List<int> GenerateQuestion(int chpWeightage,int chp)
        {

            int listValue = 0,marks = 8;

            int maxValue = question.getCount(1);

            List<int> listOfId = new List<int>();

            UpdateProbabilityAsFive();

            while (chpWeightage != 0)
            {
                int randomNumber = random.Next(1, maxValue + 1);
                string anyQuestion = question.GetQuestion(randomNumber, marks, chp);
                if (anyQuestion != null)
                {
                    chpWeightage -= marks;
                    if (chpWeightage < 0)
                    {
                        chpWeightage += marks;
                        int lastQuestionMarks = question.GetMarks(listOfId.ElementAt(listValue - 1));
                        chpWeightage += lastQuestionMarks;
                        question.Update(listOfId.ElementAt(listValue - 1), 0);
                        listOfId.RemoveAt(--listValue);
                    }
                    else
                    {
                        listOfId.Add(randomNumber);
                        Console.WriteLine("id :" + listOfId.ElementAt(listValue) + "question :" + anyQuestion + "marks : " + marks + "weight :" + chpWeightage);
                        question.Update(listOfId.ElementAt(listValue++), 10);
                    }
                    if (marks == 4)
                        marks = 8;
                    else
                        marks -= 2;
                }
            }
            return listOfId;
        }
        public void Insert()
        {
            int marks = 8;
            for (int i = 0; i < 20; i++)
            {
                question.Insert("THIS IS THE AWESOME QUESTION ..................", 3, marks);
                if (marks == 4)
                    marks = 8;
                else
                    marks -= 2;
            }
        }
    }
}
