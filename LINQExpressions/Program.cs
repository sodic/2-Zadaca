using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Classes;

namespace LINQExpressions
{
    class Program
    {


        static void Main(string[] args)
        {

            //Treci zadatak
            Console.WriteLine("Treci zadatak:\n");
            int[] integers = new[] { 1, 2, 2, 2, 3, 3, 4, 5 };
            String[] strings =
                integers.GroupBy(a => a).Select(group => $"Broj {group.Key} ponavlja se {group.Count()} puta").ToArray();
            Console.Write(string.Join("\n", strings));
            Console.WriteLine();

            //Cetvrti zadatak
            Console.WriteLine("Cetvrti zadatak:\n");
            var list = new List<Student>()
            {
                new Student(" Ivan ", jmbag: " 001234567 ")
            };
            var ivan = new Student(" Ivan ", jmbag: " 001234567 ");
            Console.WriteLine("Any Ivan Exists: {0}", list.Any(x => x == ivan));
            list.Add(new Student(" Ivan ", jmbag: " 001234567 "));
            var distinctStudents = list.Distinct().Count();
            var t = ivan.GetHashCode();
            Console.WriteLine("The list contains {0} distinct student{1}", distinctStudents, distinctStudents == 1 ? "" : "s");

            //Peti zadatak
            University[] universities = new University[]
            {
                new University(),
                new University(),
                new University(),
            };

            Student[] allCroatianStudents = universities.SelectMany(x => x.Students)
                                                        .Distinct()
                                                        .ToArray();
            Student[] croatianStudentsOnMultipleUniversities =
                universities.SelectMany(x => x.Students).GroupBy(y => y).Where(z => z.Count() > 1).Select(group => group.Key).ToArray();
            Student[] studentsOnMaleOnlyUniversities = universities.Select(x => x)
                                                                   .Where(y => y.Students
                                                                                         .Count(z => z.Gender == Gender.Female) == 0)
                                                                   .SelectMany(q => q.Students)
                                                                   .ToArray();

        }
    }
}
