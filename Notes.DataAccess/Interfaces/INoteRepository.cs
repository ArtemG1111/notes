

using Notes.DataAccess.Data.Models;

namespace Notes.DataAccess.Interfaces
{
    public interface INoteRepository
    {
        void AddNote(Note note);
        void UpdateNote(Note note);
        void DeleteNote(int id);
        List<Note> GetAllNote(string userId);
        Note GetNoteById(int id);
    }   
}
