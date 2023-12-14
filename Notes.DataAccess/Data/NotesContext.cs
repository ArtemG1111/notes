using Microsoft.EntityFrameworkCore;
using Notes.DataAccess.Data.Models;
using System.Reflection;

namespace Notes.DataAccess.Data
{
    public class NotesContext : DbContext
    {
        public DbSet<Note>? Notes { get; set; }
        public NotesContext(DbContextOptions<NotesContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
