using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EFCFirstApp
{
    public class CrudExample
    {
        public static void Run()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            var connectionString = config.GetConnectionString("DefaultConnection");
            var optionsBuilder = new DbContextOptionsBuilder<helloappdbContext>();
            var options = optionsBuilder.UseSqlServer(connectionString).Options;

            using (helloappdbContext db = new helloappdbContext(options))
            {
                User user1 = new User { Name = "Kate", Age = 27 };
                User user2 = new User { Name = "Alex", Age = 39 };

                db.Users.Add(user1);
                db.Users.Add(user2);
                db.SaveChanges();
            }

            //Read
            using (helloappdbContext db = new helloappdbContext(options))
            {
                var users = db.Users.ToList();
                Console.WriteLine("Data after insert:");
                foreach(User u in users)
                {
                    Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
                }
            }

            //Update
            using (helloappdbContext db = new helloappdbContext(options))
            {
                User user = db.Users.FirstOrDefault();
                if(user!=null)
                {
                    user.Name = "Bob";
                    user.Age = 44;
                    db.SaveChanges();
                }

                var users = db.Users.ToList();
                Console.WriteLine("Data after update:");
                foreach (User u in users)
                {
                    Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
                }
            }

            //Delete
            using (helloappdbContext db = new helloappdbContext(options))
            {
                User user = db.Users.FirstOrDefault();
                if(user!=null)
                {
                    db.Users.Remove(user);
                    db.SaveChanges();
                }

                var users = db.Users.ToList();
                Console.WriteLine("Data after delete:");
                foreach (User u in users)
                {
                    Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
                }
            }
        }
    }
}
