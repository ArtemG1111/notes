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
        public List<Note> GetAllNote(string userId)
        {
            return _context.Notes.Where(u => u.UserId == userId).ToList();
        }
        public void UpdateNote(Note note)
        {
            Note? existingNote = _context.Notes.Find(note.Id);
            if (existingNote == null)
            {
                throw new Exception ($"Error occurred while updating note, note with id {note.Id} not found");
            }
           
            existingNote.Title = note.Title;
            existingNote.Content = note.Content;
            _context.Attach(existingNote);
            _context.SaveChanges();
            
        }
        public void DeleteNote(int id)
        {
            Note? note = _context.Notes.Find(id);
            if (note == null)
            {
                throw new Exception($"Error occurred while deleting note, note with id {id} not found");
            }
            _context.Remove(note);
            _context.SaveChanges();
        }
        public Note GetNoteById(int id)
        {
            return _context.Notes.Find(id);
        }
       
    }
}
