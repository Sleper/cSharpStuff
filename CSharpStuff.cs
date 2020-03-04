using System;
using System.Collections.Generic;
using System.Data

namespace HelloWorld
{
    class Program
    {
        public class Student
        {
            private List<int> homeworkResults = new List<int>();
            private double examResult = 0;
            public Student(string name, string surname)
            {
                firstName = name;
                lastName = surname;
                homeWorkMarkAvg = 0;
                amountOfHomeworksDone = 0;
            }
            public void AddHomeWorkMark(int mark)
            {
                homeworkResults.Add(mark);
            }

            public void countHomeworkResult()
            {
                var marks = 0;
                foreach (int mark in homeworkResults)
                {
                    marks += mark;
                }
                homeWorkMarkAvg = marks / amountOfHomeworksDone;
            }
            public string firstName { get; set; }
            public string lastName { get; }
            public double homeWorkMarkAvg { get; set; }
            public int amountOfHomeworksDone { get; set; }

            public void incrementAmountOfHWDone()
            {
                amountOfHomeworksDone++;
            }
        }
        static void Main(string[] args)
        {
            var students = new List<Student>();
            var stopAddingStudents = true;

            Console.WriteLine("Add the students first name and last name");
            while(stopAddingStudents)
            {
                Console.Write("First name: ");
                var name = Console.ReadLine();
                Console.Write("Last name: ");
                var lastName = Console.ReadLine();
                students.Add(new Student(name, lastName));
                Console.Write("Would you want to add another student? (to stop type - no)  ");
                var answer = Console.ReadLine();
                if (answer.Equals("no"))
                {
                    stopAddingStudents = false;
                }
            }

            var stop = true;
            foreach (Student student in students) 
            {
                while (stop)
                {
                    Console.WriteLine("Input a homework mark for " + student.firstName + " " + student.lastName + " : ");
                    var mark = Console.ReadLine();
                    student.AddHomeWorkMark(Int32.Parse(mark));
                    student.incrementAmountOfHWDone();
                    Console.WriteLine("\n Do you wish to stop? yes? no? \n");
                    var shouldStop = Console.ReadLine();
                    if (shouldStop.Equals("yes"))
                    {
                        stop = false;
                    }
                }
                stop = true;
            }
            DataTable results = new DataTable();
            results.TableName = "Grades";
            results.Columns.Add("Surname", typeof(string));
            results.Columns.Add("Name", typeof(string));
            results.Columns.Add("Final Points (Avg.)", typeof(double));
            results.Columns.Add("Final points (Med.)", typeof(double));
            Console.WriteLine("Surname      Name           Final Points (Avg.)    Final points (Med.)");
            Console.WriteLine("----------------------------------------------------------------------");
            foreach (Student student in students)
            {
                student.countHomeworkResult();
                Console.WriteLine(student.firstName + "   " + student.lastName + "         " + student.homeWorkMarkAvg);
            }
        }
    } 
}


