

using Microsoft.AspNetCore.Identity;

namespace Notes.DataAccess.Data.Models
{
    public class User : IdentityUser
    {
        public List<Note>? UserNotes { get; set; }
    }
}
