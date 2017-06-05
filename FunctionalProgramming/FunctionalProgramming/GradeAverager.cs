using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace FunctionalProgramming
{
    class GradeAverager
    {

        List<string> classGradesList;
        List<double> studentAverageGrades;

        public void RunDemo()
        {

            classGradesList = new List<string>();
            studentAverageGrades = new List<double>();
            classGradesList.AddRange(new string[] { "8,75,100,100,95", "7,10,85,85,90", "65,70,60,75,80", "100,95,85,60,80", "1,1,1,1,1" });
            int studentNumber = 0;

            for (int i = 0; i < 5; i++)

            {
                


                //string[] studentGradesList = new string[5/*classGradesList.Count*/];

                string[] studentGradesList = ConvertStringToArray(3);     // Converts one string "80,75,90" etc into string []

                int[] studentGradesAsInts = ConvertStringsToInts(studentGradesList);  // Converts string [] into int []

                studentGradesAsInts = DropLowestGrade(studentGradesAsInts);           // Removes lowest grade

                double averageGrade = studentGradesAsInts.Average();                  // Averages student grade

                studentAverageGrades.Add(averageGrade);                               // Adds student's average grade to class list

                studentNumber++;
            }

            Console.WriteLine("\nEach student's average grade minus lowest grade:\n");

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(studentAverageGrades[i]);
            }


            //.ForEach(Console.WriteLine);
            // Console.WriteLine("\nAverage grade for the class minus lowest grade:\n");
            // Console.WriteLine(studentAverageGrades.Average());

        }

        public string[] ConvertStringToArray(int gradeGroupIndex)
        {
            string[] gradesAsStrings = classGradesList[gradeGroupIndex].Split(',');
            return gradesAsStrings;
        }


        public int[] ConvertStringsToInts(string[] grades)
        {
            int[] gradesAsInts = grades.Select(int.Parse).ToArray();
            return gradesAsInts;
        }


        public int[] DropLowestGrade(int[] grades)
        {
            List<int> gradesList = grades.ToList();

            if (grades.All(element => element == grades.Min()))
            {
                return grades;
            }
            
            else
            {
                gradesList.RemoveAll(item => item == grades.Min());
                int[] newGrades = gradesList.ToArray();
                return newGrades;
            }
        }




            //foreach (int item in grades)
            //{
            //    if (grades[item] == lowestGrade)
            //    {
            //        grades.ToList().RemoveAt(item);

            //    }                
            //}


        //int lowestGrade = gradesAsInts.Min();
        //gradesAsInts.RemoveAll(match => match == lowestGrade);
        //        gradesAsInts.ForEach(Console.WriteLine);

        //List<int> glist = new List<int>();
        //glist.AddRange(new int[] { 70, 70, 70, 70 });

        //if (gradeList.All(grade => grade == gradeList[0]))
        //{
        //    // grade is first grade
        ////}









    }
}
