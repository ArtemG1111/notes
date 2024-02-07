
using Microsoft.AspNetCore.Identity;

namespace Notes.DataAccess.Data.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public User User { get; set; } 
        public string UserId { get; set; } 
    }
}
