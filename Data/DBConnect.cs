using Mossad.Modles;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace Mossad.Data
{
    public class DBConnect: DbContext
    {
            public DBConnect(DbContextOptions<DBConnect> options) : base(options)
            {
                //if (Database.EnsureCreated() && Login.Count() == 0)
                //{ Seed(); }
            }

            public DbSet<Agent> Agents { get; set; }
            public DbSet<Target> Targets { get; set; }
            public DbSet<Mission> Missions { get; set; }


            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                //Database.EnsureCreated();

                base.OnModelCreating(modelBuilder);
            }
            //public void Seed()
            //{
            //    LoginObject login = new LoginObject()
            //    {
            //        UserName = "admin",
            //        Password = "1234"
            //    };
            //    Login.Add(login);
            //    SaveChanges();

            //}
        
    }
}
