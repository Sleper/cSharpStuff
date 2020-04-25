using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    class Student
    {
        private List<double> homeworkResults = new List<double>();
        private double finalGrade = 0;
        public Student(string name, string surname)
        {
            firstName = name;
            lastName = surname;
            homeWorkMarkAvg = 0;
            amountOfHomeworksDone = 0;
            examGrade = 0;
        }
        public void AddHomeWorkMark(double mark)
        {
            homeworkResults.Add(mark);
            amountOfHomeworksDone++;
        }

        public void addMultipleHomeWorks(double[] marks)
        {
            foreach(double mark in marks)
            {
                AddHomeWorkMark(mark);
            }
        }

        public void countHomeworkResult()
        {
            double marks = 0;
            foreach (double mark in homeworkResults)
            {
                marks += mark;
            }
            homeWorkMarkAvg = marks / amountOfHomeworksDone;
        }

        public double calculateFinalGrade()
        {
            return (0.3 * homeWorkMarkAvg) + (0.7 * examGrade);
        }

        public double calculateMedian()
        {
            homeworkResults.Add(examGrade);
            double middle = homeworkResults.Count() / 2;
            homeworkResults.Sort();
            if (homeworkResults.Count() % 2 == 0)
            {
                return (homeworkResults.ElementAt(Convert.ToInt32(middle)) + homeworkResults.ElementAt(Convert.ToInt32(middle - 1))) / 2;
            } else
            {
                return homeworkResults.ElementAt(Convert.ToInt32(Math.Floor(middle)));
            }
        }

        public string didStudentPass()
        {
            if (calculateFinalGrade() < 5)
            {
                return "Failed";
            } else
            {
                return "Passed";
            }
        }
        public string firstName { get; set; }
        public string lastName { get; }
        public double homeWorkMarkAvg { get; set; }
        public int amountOfHomeworksDone { get; set; }

        public int examGrade { get; set; }
    }
}
