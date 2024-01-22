using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Notes.BusinessLogic.Interfaces;
using Notes.DataAccess.Data.Models;
using Notes.WEB.ViewModels;

namespace Notes.WEB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NoteController
    {
        private readonly IMapper _mapper;
        private readonly INoteService _noteService;
        public NoteController(INoteService noteService, IMapper noteMapper)
        {
            _noteService = noteService;
            _mapper = noteMapper;
        }
        [HttpPost]
        public void AddNote(NoteViewModel note)
        {
            _noteService.AddNote(_mapper.Map<Note>(note));         
        }
        [HttpGet]
        public List<Note> GetAllNote(int userId)
        {
            return _noteService.GetAllNote(userId);
        }
        [HttpPut]
        public void UpdateNote(NoteViewModel note)
        {
            _noteService.UpdateNote(_mapper.Map<Note>(note));
        }
        [HttpDelete("{id}")]
        public void DeleteNote(int id)
        {
            _noteService.DeleteNote(id);
        }
        [HttpGet("{id}")]
        public Note GetNoteById(int id)
        {
            return _noteService.GetNoteById(id);
        }
    }
}
