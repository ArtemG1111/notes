
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Notes.BusinessLogic.Interfaces;
using Notes.BusinessLogic.Services;
using Notes.BusinessLogic.Servises;
using Notes.DataAccess.Data;
using Notes.DataAccess.Interfaces;
using Notes.DataAccess.Repositories;
using Notes.WEB.Common.Mappings;
using Notes.WEB.Common.Validators;
using Notes.WEB.Controllers;
using Notes.WEB.Infrastructure.Middleware.ErrorHandling;

var builder = WebApplication.CreateBuilder(args);

var connectiongString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddTransient<INoteRepository, NoteRepository>()
        .AddTransient<INoteService, NoteService>()
        .AddTransient<IUserRepository, UserRepository>()
        .AddTransient<IUserService, UserService>()
        .AddTransient<NoteController>()
        .AddTransient<UserController>()
        .AddDbContext<NotesContext>(options => options.UseSqlite(connectiongString));
    builder.Services.AddScoped<NotesContext>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(NoteMappingProfile));
builder.Services.AddValidatorsFromAssemblyContaining<NoteViewModelValidator>();

var app = builder.Build();

app.UseDeveloperExceptionPage();
app.UseRouting();
app.MapControllers();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseToken();
app.Run();
    

