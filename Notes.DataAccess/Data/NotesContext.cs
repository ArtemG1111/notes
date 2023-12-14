using Microsoft.EntityFrameworkCore;
using Notes.DataAccess.Data.Models;

namespace Notes.DataAccess.Data
{
    public class NotesContext : DbContext
    {
        public DbSet<Note>? Notes { get; set; }
        public NotesContext(DbContextOptions<NotesContext> options)
            : base(options)
        {

        }
    }
}
