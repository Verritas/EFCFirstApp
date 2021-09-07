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
                var users = await db.Users
                                    .Include(p => p.Company)
                                    .Where(p => p.CompanyId == 1)
                                    .ToListAsync();

                foreach (var user in users)
                    Console.WriteLine($"{user.Name} ({user.Age}) - {user.Company.Name}");
            }
        }
    }
}
