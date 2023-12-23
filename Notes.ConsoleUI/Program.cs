using Microsoft.EntityFrameworkCore;  
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notes.BusinessLogic.Interfaces;
using Notes.BusinessLogic.Services;
using Notes.BusinessLogic.Servises;
using Notes.ConsoleUI.Controllers;
using Notes.DataAccess.Data;
using Notes.DataAccess.Data.Models;
using Notes.DataAccess.Interfaces;
using Notes.DataAccess.Repositories;


namespace Notes.ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var config = GetConfiguration();
            var serviceProvider = GetServiceProvider(config);
            var noteController = serviceProvider.GetService<NoteController>();
            var userController = serviceProvider.GetService<UserController>();        
            Note note = new Note();
            User user = new User();
            int menu = 0;
            do
            {
                if (user.Id == 0)
                {
                    Console.WriteLine("1 - Registration | 2 - Log In");
                    Int32.TryParse(Console.ReadLine(), out menu);
                    Console.Clear();
                    switch (menu)
                    {
                        case 1:
                            Console.Write("User Name: ");
                            user.UserName = Console.ReadLine();
                            Console.Write("Password: ");
                            user.Password = Console.ReadLine();
                            userController.Registration(user);
                            Console.Clear();
                            break;
                        case 2:
                            Console.Write("User Name: ");
                            user.UserName = Console.ReadLine();
                            Console.Write("Password: ");
                            user.Password = Console.ReadLine();
                            user = userController.LogIn(user);
                            Console.Clear();
                            break;
                    }
                }

                if (user.Id != 0)
                {
                    Console.WriteLine("# # # # # Notes App # # # # #");
                    Console.WriteLine("1 - View Notes | 2 - Create Note | 3 - Update Note | 4 - Delete Note | 5 - Log Out | 0 - Exit | ");
                    Int32.TryParse(Console.ReadLine(), out menu);
                    Console.Clear();
                    switch (menu)
                    {
                        case 1:
                            Console.WriteLine("# # # # # Notes App # # # # # ");
                            var notes = noteController.GetAllNote(user.Id);
                            Console.WriteLine("Your Notes");
                            foreach (var n1 in notes)
                            {
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
                            note.Title = Console.ReadLine();
                            Console.Write("Content: ");
                            note.Content = Console.ReadLine();
                            note.UserId = user.Id;
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
                                note.Title = Console.ReadLine();
                                Console.Write("Change content: ");
                                note.Content = Console.ReadLine();
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
                            if (note != null && user.Id == note.UserId)
                            {
                                Console.WriteLine("Note deleted");
                                noteController.DeleteNote(note);
                            }
                            else
                                Console.WriteLine("Wrong Id");
                            Console.WriteLine("Press Enter to back in menu");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        case 5:
                            user.Id = 0;
                            break;
                    }
                }
            }
            while (menu != 0);


        }
        static IServiceProvider GetServiceProvider(IConfiguration config)
        {
            var connectiongString = config.GetConnectionString("DefaultConnection");
            IServiceCollection services = new ServiceCollection();
            services.AddTransient<INoteRepository, NoteRepository>()
                .AddTransient<INoteService, NoteService>()
                .AddTransient<IUserRepository, UserRepository>()
                .AddTransient<IUserService, UserService>()
                .AddTransient<NoteController>()
                .AddTransient<UserController>()
                .AddDbContext<NotesContext>(options => options.UseSqlite(connectiongString));
            services.AddScoped<NotesContext>();           
            return services.BuildServiceProvider();
            
        }
        static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            return builder.Build();
        }
    }
}
