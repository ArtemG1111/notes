using Notes.BusinessLogic.Interfaces;
using Notes.DataAccess.Data.Models;

namespace Notes.ConsoleUI.Controllers
{
    public class NoteController
    {
        private readonly INoteService _noteService;
        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }
        public void AddNote(Note note)
        {
            _noteService.AddNote(note);
        }
        public List<Note> GetAllNote()
        {
            return _noteService.GetAllNote();
        }
        public void UpdateNote(Note note)
        {
            _noteService.UpdateNote(note);
        }
        public void DeleteNote(Note note)
        {
            _noteService.DeleteNote(note);
        }
        public Note GetNoteById(int id)
        {
            return _noteService.GetNoteById(id);
        }
    }
}
