using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Notes.BusinessLogic.Interfaces;
using Notes.DataAccess.Data.Models;
using Notes.WEB.ViewModels;

namespace Notes.WEB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NoteController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly INoteService _noteService;
        private readonly ILogger<NoteController> _logger;
        public NoteController(INoteService noteService, IMapper noteMapper, ILogger<NoteController> logger)
        {
            _noteService = noteService;
            _mapper = noteMapper;
            _logger = logger;
        }
        [HttpPost]
        public IActionResult AddNote(NoteViewModel note)
        {
            _noteService.AddNote(_mapper.Map<Note>(note));
            _logger.LogInformation($"Note successfully added");
            return Ok($"Note successfully added");
        }
        [HttpGet]
        public List<Note> GetAllNote(int userId)
        {
            return _noteService.GetAllNote(userId);
        }
        [HttpPut]
        public IActionResult UpdateNote(NoteViewModel note)
        {
            _noteService.UpdateNote(_mapper.Map<Note>(note));
            _logger.LogInformation($"Note was updated");
            return Ok($"Note was updated");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteNote(int id)
        {
            _noteService.DeleteNote(id);
            _logger.LogInformation("Note was deleted");
            return Ok("Note was deleted");
        }
        [HttpGet("{id}")]
        public Note GetNoteById(int id)
        {
            return _noteService.GetNoteById(id);
        }
    }
}
