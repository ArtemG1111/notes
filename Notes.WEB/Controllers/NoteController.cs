using Microsoft.AspNetCore.Mvc;
using Notes.BusinessLogic.Interfaces;
using Notes.DataAccess.Data.Models;

namespace Notes.WEB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NoteController
    {
        private readonly INoteService _noteService;
        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }
        [HttpPost]
        public void AddNote(Note note)
        {
            _noteService.AddNote(note);
        }
        [HttpGet]
        public List<Note> GetAllNote(int userId)
        {
            return _noteService.GetAllNote(userId);
        }
        [HttpPut]
        public void UpdateNote(Note note)
        {
            _noteService.UpdateNote(note);
        }
        [HttpDelete]
        public void DeleteNote(Note note)
        {
            _noteService.DeleteNote(note);
        }
        [HttpGet("{id}")]
        public Note GetNoteById(int id)
        {
            return _noteService.GetNoteById(id);
        }
    }
}
