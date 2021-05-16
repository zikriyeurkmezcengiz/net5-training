using System;
using System.Linq;
using Linq_Practices.DBOperations;
using Linq_Practices.Entities;

namespace Linq_Practices
{
    class Program
    {
        static void Main(string[] args)
        {
            LinqDbContext _context = new LinqDbContext();
            DataGenerator.Initialize();
            var students = _context.Students.ToList<Student>();

            //Find
            Console.WriteLine();
            Console.WriteLine("***** Find *****");
            var student = _context.Students.Find(1);
            Console.WriteLine(student.Name);

            //FirstOrDefault()
            Console.WriteLine();
            Console.WriteLine("***** FirstOrDefault *****");
            student = _context.Students.Where(student => student.Surname == "Arda").FirstOrDefault();
            Console.WriteLine(student.Name);

            student = _context.Students.FirstOrDefault(student => student.Surname == "Arda");
            Console.WriteLine(student.Name);

            //SingleOrDefault
            Console.WriteLine();
            Console.WriteLine("***** SingleOrDefault *****");
            student = _context.Students.SingleOrDefault(student => student.Name == "Deniz");
            Console.WriteLine(student.Name);

            //ToList
            Console.WriteLine();
            Console.WriteLine("***** ToList *****");
            var studentList = _context.Students.Where(student => student.ClassId == 2).ToList();
            Console.WriteLine(studentList.Count);

            //OrderBy
            Console.WriteLine();
            Console.WriteLine("***** OrderBy *****");

            students = _context.Students.OrderBy(x => x.StudentId).ToList();
            foreach (var st in students)
            {
                Console.WriteLine(st.StudentId + " - " + st.Name + " " + st.Surname);
            }

            //OrderByDesc
            Console.WriteLine();
            Console.WriteLine("***** OrderByDesc *****");
            students = _context.Students.OrderByDescending(x => x.StudentId).ToList();
            foreach (var st in students)
            {
                Console.WriteLine(st.StudentId + " - " + st.Name + " " + st.Surname);
            }


            //Anonymous Object Result
            Console.WriteLine();
            Console.WriteLine("***** Anonymous Object Result *****");
            var anonymousObjResult = _context.Students
                                   .Where(b => b.ClassId == 2)
                                   .Select(b => new
                                   {

                                       Id = b.StudentId,
                                       FullName = b.Name + " " + b.Surname
                                   });

            foreach (var obj in anonymousObjResult)
            {
                Console.WriteLine(obj.Id + " - " + obj.FullName);
            }

            // THE END
        }
    }
}
