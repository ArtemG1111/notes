using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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
            using var db = new NotesContext(options);

            dbContext.Database.EnsureCreated(); 
            Note note = new Note();
            int menu;
            do
            {
                Console.WriteLine("# # # # # Notes App # # # # #");
                Console.Write("1 - View Notes | 2 - Create Note | 3 - Update Note | 4 - Delete Note | 0 - Exit | ");
                Int32.TryParse(Console.ReadLine(), out menu);               
                Console.Clear();
                switch (menu)
                {
                    case 1:                      
                        Console.WriteLine("# # # # # Notes App # # # # # ");
                        var notes = noteController.GetAllNote();
                        foreach (var n1 in notes)
                        {
                            Console.WriteLine("Your Notes");
                            Console.WriteLine("---------------");
                            Console.WriteLine($"Id:{n1.Id}");
                            Console.WriteLine($"Title:{n1.Title}");
                            Console.WriteLine($"Content:{n1.Content}");
                        }
                        Console.WriteLine("Press Enter to back in menu");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 2:
                        Console.WriteLine("# # # # # Notes App # # # # # ");
                        Console.WriteLine("Creating new note");
                        note = new Note();
                        Console.Write("Title: ");
                        Console.WriteLine("", note.Title = Console.ReadLine());
                        Console.Write("Content: ");
                        Console.WriteLine("", note.Content = Console.ReadLine());
                        noteController.AddNote(note);
                        Console.WriteLine("Press Enter to back in menu");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 3:
                        Console.WriteLine("# # # # # Notes App # # # # # ");
                        Console.WriteLine("Update note");
                        Console.Write("Enter note Id: ");
                        note = noteController.GetNoteById(Convert.ToInt32(Console.ReadLine()));
                        if (note != null)
                        {
                            Console.WriteLine("Note found!");
                            Console.Write("New title: ");
                            Console.WriteLine("", note.Title = Console.ReadLine());
                            Console.WriteLine("Change content: ");
                            Console.WriteLine("", note.Content = Console.ReadLine());
                            noteController.UpdateNote(note);
                        }
                        else
                            Console.WriteLine("Wrong Id");
                        Console.WriteLine("Press Enter to back in menu");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 4:
                        Console.WriteLine("# # # # # Notes App # # # # # ");
                        Console.WriteLine("Delete note");
                        Console.Write("Enter note Id: ");
                        note = noteController.GetNoteById(Convert.ToInt32(Console.ReadLine()));
                        if (note != null)
                        {
                            Console.WriteLine("Once deleted, the note is gone forever. Are you sure?");
                            Console.Write("yes/no: ");
                            string? answer = Console.ReadLine();
                            if (answer == "yes")
                            {
                                noteController.DeleteNote(note);
                            }
                            else if (answer == "no")
                            {
                                Console.WriteLine("Press Enter");
                                Console.ReadLine();
                            }
                        }
                        else
                            Console.WriteLine("Wrong Id");
                        Console.WriteLine("Press Enter to back in menu");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                }
            }
            while (menu != 0);


        }
    }
}