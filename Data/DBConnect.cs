using Mossad.Models;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace Mossad.Data
{
    public class DBConnect: DbContext
    {
            public DBConnect(DbContextOptions<DBConnect> options) : base(options)
            {
                if (Database.EnsureCreated() && Agents.Count() == 0)
                { Seed(); }
            }

            public DbSet<Agent> Agents { get; set; }
            public DbSet<Target> Targets { get; set; }
            public DbSet<Mission> Missions { get; set; }
            public DbSet<User> Users {  get; set; }


            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                //Database.EnsureCreated();

                base.OnModelCreating(modelBuilder);
            }
            public void Seed()
            {
                Agent agent =  new Agent()
                { 
                    Name = "Yair",
                    Image = "qwertyuio",
                    Status = false
                };
                Agents.Add(agent);
                SaveChanges();
            }
    }
}
