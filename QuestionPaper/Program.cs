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
        static void Main(string[] args)
        {
            int howManyQue = 20;
            var listOfId = new List<int>();
            Question question= new Question();
            //question.Insert("THIS IS THE AWESOME QUESTION ..................", 3, 8);
            Random random = new Random();
            int maxValue = question.getCount();
            for (int i = 0; i < howManyQue; i++)
            {
                int randomNumber = random.Next(1, maxValue);
                listOfId.Add(randomNumber);
                string anyQuestion = question.GetQuestion(randomNumber);
                if(anyQuestion == null)
                {
                    howManyQue++;
                }
                Console.WriteLine(listOfId.ElementAt(i)+ anyQuestion);
            }
            MessageBox.Show(howManyQue+"");
            if (question.GetProbabilityCount(10).Count() != 0)
            {
                var list = question.GetProbabilityCount(10);
                for(int i =0;i<list.Count();i++)
                    question.Update(list.ElementAt(i),5);  
            }
            for (int i = 0; i < listOfId.Count(); i++)
                question.Update(listOfId.ElementAt(i),10);
            Console.Read();
        }
    }
}
