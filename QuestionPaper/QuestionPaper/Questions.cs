using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionPaper
{
    class Questions
    {
        public Questions(string question, int marks,int id,int chapterno,int probability)
        {
            this.question = question;
            this.marks = marks;
            this.id = id;
            this.chapterno= chapterno;
            this.probability= probability;
        }
        public string question { set; get; }
        public int marks { set; get; }
        public int id { set; get; }
        public int chapterno { set; get; }
        public int probability { set; get; }
        

    }
}
