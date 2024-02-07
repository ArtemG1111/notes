using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Notes.DataAccess.Data.Models;
using System.Reflection;

namespace Notes.DataAccess.Data
{
    public class NotesContext : IdentityDbContext<User>
    {
        public DbSet<Note> Notes { get; set; }

        public NotesContext(DbContextOptions<NotesContext> options)
            : base(options) 
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
