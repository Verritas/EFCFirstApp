using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace EFCFirstApp
{
    public class ManyToManyExample
    {
        public static void Run()
        {
            using (ManyToManydbContext db = new ManyToManydbContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                Student tom = new Student { Name = "Tom" };
                Student alice = new Student { Name = "Alice" };
                Student bob = new Student { Name = "Bob" };

                db.Students.AddRange(tom, alice, bob);

                Course algorithms = new Course { Name = "Algorithms" };
                Course basics = new Course { Name = "Basics of programming" };
                db.Courses.AddRange(algorithms, basics);

                tom.Courses.Add(algorithms);
                tom.Courses.Add(basics);

                alice.Courses.Add(algorithms);

                bob.Courses.Add(basics);

                db.SaveChanges();
            }

            using(ManyToManydbContext db = new ManyToManydbContext())
            {
                Student alice = db.Students.Include(s => s.Courses).FirstOrDefault(s => s.Name == "Alice");
                Course algorithms = db.Courses.FirstOrDefault(c=>c.Name ==  "Algorithms");
                Course basics = db.Courses.FirstOrDefault(c => c.Name == "Basics of programming");
                if (alice!=null && algorithms!=null && basics!=null)
                {
                    if (algorithms != null)
                    {
                        alice.Courses.Remove(algorithms);
                    }
                    if (basics != null)
                    {
                        alice.Courses.Add(basics);
                    }

                    db.SaveChanges();
                }
            }

            using (ManyToManydbContext db = new ManyToManydbContext())
            {
                var courses = db.Courses.Include(c=>c.Students).ToList();
                
                foreach(var c in courses)
                {
                    Console.WriteLine($"Course: {c.Name}");
                    foreach(Student s in c.Students)
                    {
                        Console.WriteLine($"Name: {s.Name}");
                    }

                    Console.WriteLine("-------------");
                }
            }
        }
    }
}
