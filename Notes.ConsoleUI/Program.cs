using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Notes.BusinessLogic.Servises;
using Notes.ConsoleUI.Controllers;
using Notes.DataAccess.Data;
using Notes.DataAccess.Data.Models;
using Notes.DataAccess.Repositories;

namespace Notes.ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            var connectiongString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<NotesContext>();
            var options = optionsBuilder.UseSqlite(connectiongString).Options;

            using var dbContext = new NotesContext(options);
            var noteRepository = new NoteRepository(dbContext);
            var noteService = new NoteService(noteRepository);
            var noteController = new NoteController(noteService);

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            Note note1 = new Note { Content = "111", Title = "112" };
            Note note2 = new Note { Content = "222", Title = "221" };
            Note note3 = new Note { Content = "333", Title = "313" };

            noteController.AddNote(note1);
            noteController.AddNote(note2);
            noteController.AddNote(note3);

            var notes = noteController.GetAllNote();
            foreach (var n in notes)
            {
                Console.WriteLine($"{n.Content} - {n.Title}");
            }
            Console.WriteLine("После удаления");
            noteController.DeleteNote(note2);
            
            foreach (var n in notes)
            {
                Console.WriteLine($"{n.Content} - {n.Title}");
            }
            Console.WriteLine("После обновления");
            var update = noteController.GetNoteById(1);
            if (update != null)
            {
                update.Content = "444";
                update.Title = "222";
                noteController.UpdateNote(update);
            }
            
            foreach (var n in notes)
            {
                Console.WriteLine($"{n.Content} - {n.Title}");
            }
            Console.WriteLine("По Id");
            var note = noteController.GetNoteById(1);
            Console.WriteLine($"{note.Content} - {note.Title}");
            //test

        }
    }
}