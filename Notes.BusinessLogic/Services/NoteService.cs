using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Notes.BusinessLogic.Interfaces;
using Notes.DataAccess.Data.Models;
using Notes.DataAccess.Interfaces;

namespace Notes.BusinessLogic.Servises
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<NoteService> _logger;
        public NoteService(INoteRepository noteRepository, IMemoryCache memoryCache, ILogger<NoteService> logger)
        {
            _noteRepository = noteRepository;
            _memoryCache = memoryCache;
            _logger = logger;
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
            _memoryCache.TryGetValue(userId, out List<Note>? notes);
            if (notes != null)
            {
                _logger.LogInformation("Data form cache");
                return notes;
            }  
            _logger.LogInformation("Data from database");
            _memoryCache.Set(userId, notes, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
            return _noteRepository.GetAllNote(userId);
        }
        public Note GetNoteById(int id)
        {
            return _noteRepository.GetNoteById(id);
        }
    }
}
