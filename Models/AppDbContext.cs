using E_CounsellingWebApplication.Hubs;
using E_CounsellingWebApplication.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_CounsellingWebApplication.Models
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
        public DbSet<Broadcast> Broadcasts { get; set; }
        public DbSet<Book> books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<UserCategory> UserCategories { get; set; }
        public DbSet<ChatConnection> Connections { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Ratings> Ratings { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<UserCounselors> UserCounselors { get; set; }
        public DbSet<Payment> payments { get; set; }


        //Enforce ON DELETE NO ACTION in entity framework core
        //if role is deleted with users in this role exception is thrown
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {


                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;

                //base.OnModelCreating(builder);
                //builder.Entity<Message>()
                //    .HasOne<AppUser>(a => a.Sender)
                //    .WithMany(d => d.Messages)
                //    .HasForeignKey(d => d.UserID);

            }
        }

    }

}
