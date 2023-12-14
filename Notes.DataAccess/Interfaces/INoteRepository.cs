

using Notes.DataAccess.Data.Models;

namespace Notes.DataAccess.Interfaces
{
    public interface INoteRepository
    {
        void AddNote(Note note);
        void UpdateNote(Note note);
        void DeleteNote(Note note);
        List<Note> GetAllNote();
        Note GetNoteById(int id);
    }
    
}
