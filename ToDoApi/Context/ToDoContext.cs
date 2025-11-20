using ToDoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ToDoApi.Context
{
    public class ToDoContext(DbContextOptions<ToDoContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<TaskToDo> Tasks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Models.TaskToDo>()
                .HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId);

        }

    }
}
