using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            generateLinesOfStudents();
            programExecution();
        }

        static void generateLinesOfStudents()
        {
            var firstname = "name";
            var lastname = "last";
            Random rnd = new Random();
            var fileNameStudents = "C:\\Users\\Algirdas\\Desktop\\student1000.txt";
            using ( var stream = File.OpenWrite(fileNameStudents))
            {
                using (var writer = new StreamWriter(stream))
                {
                    for (var i = 0; i < 1000; i++)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append(firstname + i + " ");
                        sb.Append(lastname + i + " ");
                        sb.Append(rnd.Next(1, 10) + " ");
                        sb.Append(rnd.Next(1, 10) + " ");
                        sb.Append(rnd.Next(1, 10) + " ");
                        sb.Append(rnd.Next(1, 10) + " ");
                        sb.Append(rnd.Next(1, 10) + " ");
                        sb.Append(rnd.Next(1, 10) + " ");
                        writer.WriteLine(sb);
                    }
                }
            }
        }

        static void programExecution()
        {
            Console.WriteLine("Choose a numeric option: ");
            Console.WriteLine("1. Adding students with Array based containers");
            Console.WriteLine("2. Adding students with List based containers");
            Console.WriteLine("3. Adding students from a text file");

            switch (GetOption())
            {
                case 1:
                    break;
                case 2:
                    addStudentsWithList();
                    break;
                case 3:
                    addStudentsFromFile();
                    break;
                default:
                    break;
            }
        }

        static void addStudentsFromFile()
        {
            string textFile = "C:\\Users\\Algirdas\\Desktop\\student1000.txt";
            var students = new List<Student>();

            using (StreamReader sr = new StreamReader(textFile))
            {
                string headerLine = sr.ReadLine();
                string line;
                int counter = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    var parts = line.Split(' ');
                    Student student = new Student(parts[1], parts[0]);
                    double[] grades = new double[] { Double.Parse(parts[2]), Double.Parse(parts[3]), Double.Parse(parts[4]), Double.Parse(parts[5]), Double.Parse(parts[6]) };
                    student.addMultipleHomeWorks(grades);
                    student.examGrade = Int32.Parse(parts[7]);
                    students.Add(student);
                }
            }
            chooseSortOptions(students);
            Console.WriteLine("|{0,-15}|{1,-15}|{2,20}|{3,20}|{4,10}|", "Name", "Surname", "Final Points (Avg.)", "Final points (Med.)", "Exam");
            Console.WriteLine("-------------------------------------------------------------------------------------");
            var studentsThatPassed = new List<Student>();
            var studentsThatFailed = new List<Student>();
            foreach (Student student in students)
            {
                student.countHomeworkResult();
                Console.WriteLine(string.Format("|{0,-15}|{1,-15}|{2,20}|{3,20}|{4,10}|", student.firstName, student.lastName, student.calculateFinalGrade(), student.calculateMedian(), student.didStudentPass()));
                if (student.calculateFinalGrade() < 5)
                {
                    studentsThatFailed.Add(student);
                }
                else
                {
                    studentsThatPassed.Add(student);
                }
            }

            var passedStudents = "C:\\Users\\Algirdas\\Desktop\\passedStudents.txt";
            var failedStudents = "C:\\Users\\Algirdas\\Desktop\\failedStudents.txt";

            passingStudentsToFile(studentsThatFailed, failedStudents);
            passingStudentsToFile(studentsThatPassed, passedStudents);
        }

        static void passingStudentsToFile(List<Student> students, String fileName)
        {
            using (var stream = File.OpenWrite(fileName))
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.WriteLine("|{0,-15}|{1,-15}|{2,20}|{3,20}|{4,10}|", "Name", "Surname", "Final Points (Avg.)", "Final points (Med.)", "Exam");
                    writer.WriteLine("-------------------------------------------------------------------------------------");
                    foreach (Student student in students)
                    {
                        writer.WriteLine(string.Format("|{0,-15}|{1,-15}|{2,20}|{3,20}|{4,10}|", student.firstName, student.lastName, student.calculateFinalGrade(), student.calculateMedian(), student.didStudentPass()));
                    }
                }
            }
        }

        static void chooseSortOptions(List<Student> students)
        {
            Console.WriteLine("Choose a numeric option: ");
            Console.WriteLine("1. Sort by First name");
            Console.WriteLine("2. Sort by Last name");

            switch (GetOption())
            {
                case 1:
                    students.Sort((x, y) => x.firstName.CompareTo(y.firstName));
                    break;
                case 2:
                    students.Sort((x, y) => x.lastName.CompareTo(y.lastName));
                    break;
                default:
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
                    try
                    {
                        student.AddHomeWorkMark(getHomeworkMark());
                    } catch (FormatException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    Console.WriteLine("\n Do you wish to stop? yes? no? \n");
                    var shouldStop = Console.ReadLine().ToUpper();
                    if (shouldStop.Equals("YES"))
                    {
                        stop = false;
                    }
                }
                Console.WriteLine("Input exam mark for " + student.firstName + " " + student.lastName + " : ");
                student.examGrade = GetOption();
  
                stop = true;
            }

            Console.WriteLine("|{0,-15}|{1,-15}|{2,20}|{3,20}|{4,10}|", "Name", "Surname", "Final Points (Avg.)", "Final points (Med.)", "Exam");
            Console.WriteLine("-------------------------------------------------------------------------------------");
            var studentsThatPassed = new List<Student>();
            var studentsThatFailed = new List<Student>();
            foreach (Student student in students)
            {
                student.countHomeworkResult();
                Console.WriteLine(string.Format("|{0,-15}|{1,-15}|{2,20}|{3,20}|{4,10}|", student.firstName, student.lastName, student.calculateFinalGrade(), student.calculateMedian(), student.didStudentPass()));
                if (student.calculateFinalGrade() < 5)
                {
                    studentsThatFailed.Add(student);
                } else
                {
                    studentsThatPassed.Add(student);
                }
            }

            var passedStudents = "C:\\Users\\Algirdas\\Desktop\\passedStudents.txt";
            var failedStudents = "C:\\Users\\Algirdas\\Desktop\\failedStudents.txt";

            using (var stream = File.OpenWrite(passedStudents))
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.WriteLine("|{0,-15}|{1,-15}|{2,20}|{3,20}|", "Name", "Surname", "Final Points (Avg.)", "Final points (Med.)");
                    writer.WriteLine("-------------------------------------------------------------------------------------");
                    foreach (Student student in studentsThatPassed)
                    {
                        writer.WriteLine(string.Format("|{0,-15}|{1,-15}|{2,20}|{3,20}|", student.firstName, student.lastName, student.calculateFinalGrade(), student.calculateMedian()));
                    }
                }
            }
            using (var stream = File.OpenWrite(failedStudents))
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.WriteLine("|{0,-15}|{1,-15}|{2,20}|{3,20}|", "Name", "Surname", "Final Points (Avg.)", "Final points (Med.)");
                    writer.WriteLine("-------------------------------------------------------------------------------------");
                    foreach (Student student in studentsThatFailed)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append(string.Format("|{0,-15}|{1,-15}|{2,20}|{3,20}|", student.firstName, student.lastName, student.calculateFinalGrade(), student.calculateMedian()));
                        writer.WriteLine();
                    }
                }
            }
        }

        public static int GetOption()
        {
            try
            {
                return Int32.Parse(Console.ReadLine());
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
        }

        public static double getHomeworkMark()
        {
            return Double.Parse(Console.ReadLine());
        }
    }
}
