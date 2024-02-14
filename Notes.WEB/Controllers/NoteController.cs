using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Notes.BusinessLogic.Interfaces;
using Notes.DataAccess.Data.Models;
using Notes.WEB.ViewModels;
using System.Text.Json;

namespace Notes.WEB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NoteController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly INoteService _noteService;
        private readonly ILogger<NoteController> _logger;
        private readonly IValidator<NoteViewModel> _validator;

        public NoteController(INoteService noteService, IMapper noteMapper, ILogger<NoteController> logger
           ,IValidator<NoteViewModel> validator)
        {
            _noteService = noteService;
            _mapper = noteMapper;
            _logger = logger;
            _validator = validator;
        }
        [HttpPost]
        public async Task<IActionResult> AddNote(NoteViewModel note)
        {
            ValidationResult result = await _validator.ValidateAsync(note);            
            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => new { Property = e.PropertyName, ErrorMessage = e.ErrorMessage });
                return BadRequest(new { Errors = errors });
            }
            
            _noteService.AddNote(_mapper.Map<Note>(note));
            _logger.LogInformation($"Note successfully added");
            return Ok($"Note successfully added");
        }
        [HttpGet]
        public List<Note> GetAllNote(string userId)
        {
            return _noteService.GetAllNote(userId);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateNote(NoteViewModel note)
        {
            ValidationResult result = await _validator.ValidateAsync(note);
            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => new { Property = e.PropertyName, ErrorMessage = e.ErrorMessage });
                return BadRequest(new { Errors = errors });
            }
            
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
