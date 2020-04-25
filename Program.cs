using System;
using System.Collections.Generic;
using System.Data;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            programExecution();
            
        }

        static void programExecution()
        {
            Console.WriteLine("Choose a numeric option: ");
            Console.WriteLine("1. Adding students with Array based containers");
            Console.WriteLine("2. Adding students with List based containers");
            Console.WriteLine("3. Adding students from a text file");
            int option = Int32.Parse(Console.ReadLine());
            
            switch(option)
            {
                case 1:
                    break;
                case 2:
                    addStudentsWithList();
                    break;
                case 3:
                    break;
            }
        }

        static void addStudentsWithList()
        {
            var students = new List<Student>();
            var stopAddingStudents = true;

            Console.WriteLine("Add the students first name and last name");
            while (stopAddingStudents)
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
                    student.AddHomeWorkMark(Double.Parse(mark));
                    Console.WriteLine("\n Do you wish to stop? yes? no? \n");
                    var shouldStop = Console.ReadLine().ToUpper();
                    if (shouldStop.Equals("YES"))
                    {
                        stop = false;
                    }
                }
                Console.WriteLine("Input exam mark for " + student.firstName + " " + student.lastName + " : ");
                var examMark = Console.ReadLine();
                student.examGrade = Int32.Parse(examMark);
                stop = true;
            }
            Console.WriteLine("|{0,-15}|{1,-15}|{2,20}|{3,20}|", "Name", "Surname", "Final Points (Avg.)", "Final points (Med.)");
            Console.WriteLine("-------------------------------------------------------------------------------------");
            foreach (Student student in students)
            {
                student.countHomeworkResult();
                Console.WriteLine(string.Format("|{0,-15}|{1,-15}|{2,20}|{3,20}|", student.firstName, student.lastName, student.calculateFinalGrade(), student.calculateMedian()));
            }
        }
    } 
}
