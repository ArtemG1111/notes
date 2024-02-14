
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Notes.BusinessLogic.Interfaces;
using Notes.BusinessLogic.Services;
using Notes.BusinessLogic.Servises;
using Notes.DataAccess.Data;
using Notes.DataAccess.Data.Models;
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
builder.Services.AddValidatorsFromAssemblyContaining<NoteViewModelValidator>()
    .AddValidatorsFromAssemblyContaining<UserViewModelValidator>();
builder.Services.AddIdentity<User, IdentityRole>(opts =>
{
    opts.Password.RequireNonAlphanumeric = false;
    opts.Password.RequireLowercase = false;
    opts.Password.RequireUppercase = false;
    opts.Password.RequireDigit = false;
})
    .AddEntityFrameworkStores<NotesContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddMemoryCache();


var app = builder.Build();

app.UseDeveloperExceptionPage();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseToken();
app.Run();
    

