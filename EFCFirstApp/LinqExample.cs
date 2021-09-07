using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EFCFirstApp
{
    public class LinqExample
    {
        public static async Task RunAsync()
        {
            using (linqExampledbContext db = new linqExampledbContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                Company_Linq microsoft = new Company_Linq { Name = "Microsoft" };
                Company_Linq google = new Company_Linq { Name = "Google" };
                db.Companies.AddRange(microsoft, google);

                User_Linq tom = new User_Linq { Name = "Tom", Age = 36, Company = microsoft };
                User_Linq bob = new User_Linq { Name = "Bob", Age = 39, Company = google };
                User_Linq alice = new User_Linq { Name = "Alice", Age = 28, Company = microsoft };
                User_Linq kate = new User_Linq { Name = "Kate", Age = 25, Company = google };

                db.Users.AddRange(tom, bob, alice, kate);

                db.SaveChanges();
            }

            using (linqExampledbContext db = new linqExampledbContext())
            {
                var users = db.Users.Where(p => EF.Functions.Like(p.Name, "%Tom%"));
                foreach (User_Linq user in users)
                {
                    Console.WriteLine($"{user.Name} ({user.Age})");
                }

                users = db.Users.OrderBy(p => p.Name);
                foreach (var user in users)
                {
                    Console.WriteLine($"{user.Id}.{user.Name} ({user.Age})");
                }
            }

            using (linqExampledbContext db = new linqExampledbContext())
            {
                var users = db.Users.Join(db.Companies,
                    u => u.CompanyId,
                    c => c.Id,
                    (u, c) => new
                    {
                        Name = u.Name,
                        Company = c.Name,
                        Age = u.Age
                    });

                foreach (var u in users)
                {
                    Console.WriteLine($"{u.Name} ({u.Company}) - {u.Age}");
                }
            }

            using (linqExampledbContext db = new linqExampledbContext())
            {
                var groups = from u in db.Users
                             group u by u.Company.Name into g
                             select new
                             {
                                 g.Key,
                                 Count = g.Count()
                             };

                foreach (var group in groups)
                {
                    Console.WriteLine($"{group.Key} - {group.Count}");
                }
            }

            using (linqExampledbContext db = new linqExampledbContext())
            {
                var users = db.Users.Where(u => u.Age < 30)
                    .Union(db.Users.Where(u => u.Name.Contains("Tom")));
                foreach (var user in users)
                {
                    Console.WriteLine(user.Name);
                }
            }

            using (linqExampledbContext db = new linqExampledbContext())
            {
                var users = db.Users.Where(u => u.Age > 30)
                    .Intersect(db.Users.Where(u => u.Name.Contains("Tom")));
                foreach (var user in users)
                {
                    Console.WriteLine(user.Name);
                }
            }

            using (linqExampledbContext db = new linqExampledbContext())
            {
                var selector1 = db.Users.Where(u => u.Age > 30);
                var selector2 = db.Users.Where(u => u.Name.Contains("Tom"));
                var users = selector1.Except(selector2);

                foreach (var user in users)
                {
                    Console.WriteLine(user.Name);
                }
            }
        }
    }
}
