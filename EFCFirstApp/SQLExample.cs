using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace EFCFirstApp
{
    public class SQLExample
    {
        public static void Run()
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
                User_Linq tomas = new User_Linq { Name = "Tomas", Age = 22, Company = microsoft };
                User_Linq tomek = new User_Linq { Name = "Tomek", Age = 42, Company = google };

                db.Users.AddRange(tom, bob, alice, kate, tomas, tomek);
                db.SaveChanges();
            }

            using (linqExampledbContext db = new linqExampledbContext())
            {
                var comps = db.Companies.FromSqlRaw("SELECT * FROM Companies").ToList();
                foreach (var company in comps)
                {
                    Console.WriteLine(company.Name);
                }
            }

            using (linqExampledbContext db = new linqExampledbContext())
            {
                SqlParameter param = new SqlParameter("@name", "%Tom%");
                var users = db.Users.FromSqlRaw("SELECT * FROM Users WHERE Name LIKE @name", param).ToList();
                foreach (var user in users)
                {
                    Console.WriteLine(user.Name);
                }
            }

            using (linqExampledbContext db = new linqExampledbContext())
            {
                var age = 30;
                var users = db.Users.FromSqlRaw("SELECT * FROM Users WHERE Age > {0}", age).ToList();
                foreach (var user in users)
                {
                    Console.WriteLine(user.Name);
                }
            }

            using (linqExampledbContext db = new linqExampledbContext())
            {
                string newComp = "Apple";
                int numberOfRowInserted = db.Database.ExecuteSqlRaw("INSERT INTO Companies (Name) VALUES ({0})", newComp);

                string appleInc = "Apple Inc.";
                string apple = "Apple";
                int numberOfRowUpdated = db.Database.ExecuteSqlRaw("UPDATE Companies SET Name={0} WHERE Name={1}", appleInc, apple);

                int numberOfRowDeleted = db.Database.ExecuteSqlRaw("DELETE FROM Companies WHERE Name={0}", appleInc);
            }
        }
    }
}
