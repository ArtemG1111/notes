using Microsoft.EntityFrameworkCore;
using Notes.DataAccess.Data;
using Notes.DataAccess.Data.Models;
using Notes.DataAccess.Interfaces;

namespace Notes.DataAccess.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly NotesContext _context;
        public NoteRepository(NotesContext context)
        {
            _context = context;
        }
        public void AddNote(Note note)
        {
            _context.Add(note);
            _context.SaveChanges();   
        }
        public List<Note> GetAllNote(int userId)
        {
            return _context.Notes.Where(u => u.UserId == userId).ToList();
        }
        public void UpdateNote(Note note)
        {
            _context.Attach(note);
            _context.SaveChanges();
        }
        public void DeleteNote(Note note)
        {
            _context.Remove(note);
            _context.SaveChanges();
        }
        public Note GetNoteById(int id)
        {
            return _context.Notes.Find(id);
        }
       
    }
}
