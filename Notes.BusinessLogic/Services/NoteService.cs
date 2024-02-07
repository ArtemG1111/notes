using Notes.BusinessLogic.Interfaces;
using Notes.DataAccess.Data.Models;
using Notes.DataAccess.Interfaces;

namespace Notes.BusinessLogic.Servises
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;
        public NoteService(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }
        public void AddNote(Note note)
        {
           _noteRepository.AddNote(note);
        }
        public void UpdateNote(Note note)
        {
            _noteRepository.UpdateNote(note);
        }
        public void DeleteNote(int id)
        {    
            _noteRepository.DeleteNote(id);
        }
        public List<Note> GetAllNote(string userId)
        {
            return _noteRepository.GetAllNote(userId);
        }
        public Note GetNoteById(int id)
        {
            return _noteRepository.GetNoteById(id);
        }
    }
}
