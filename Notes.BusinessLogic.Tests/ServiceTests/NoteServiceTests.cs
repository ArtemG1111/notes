

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;
using Notes.BusinessLogic.Servises;
using Notes.DataAccess.Data;
using Notes.DataAccess.Data.Models;
using Notes.DataAccess.Interfaces;
using Notes.DataAccess.Repositories;

namespace Notes.BusinessLogic.Tests.ServiceTests
{
    public class NoteServiceTests
    {
        [Fact]
        public void GetNoteByIdTest()
        {
            //Arrange
            Note note = new();
            var mock = new Mock<INoteRepository>();
            mock.Setup(r => r.GetNoteById(1)).Returns(note);
            var cacheMock = new Mock<IMemoryCache>();
            var loggerMock = new Mock<ILogger<NoteService>>();
            var service = new NoteService(mock.Object, cacheMock.Object, loggerMock.Object);

            //Act
            var result = service.GetNoteById(1);

            //Assert
            Assert.Equal(note, result);
        }
        [Fact]
        public void GetAllNotesTest()
        {
            //Arrange
            string userId = "1";
            List<Note> notes = new List<Note>()
            {
                new Note {Content = "test", Title = "test"},
                new Note {Content = "test", Title = "test"}
            };
            var mock = new Mock<INoteRepository>();
            mock.Setup(s=>s.GetAllNote(userId)).Returns(notes);

            var cacheMock = new Mock<IMemoryCache>();
            var entryCacheMock = new Mock<ICacheEntry>();
            cacheMock.Setup(s => s.CreateEntry(userId)).Returns(entryCacheMock.Object);

            var loggerMock = new Mock<ILogger<NoteService>>();
            var service = new NoteService(mock.Object, cacheMock.Object, loggerMock.Object);

            //Act
            var result = service.GetAllNote(userId);
            cacheMock.Verify(m => m.CreateEntry(userId), Times.Once);

            //Assert
            Assert.Equal(notes, result);
        }
        [Fact]
        public void AddNoteTest()
        {
            //Arrange
            List<Note> notes = new List<Note>()
            {
                new Note(),
                new Note()
            };

            Note note = new Note();

            var contextMock = new Mock<NotesContext>();
            contextMock.Setup(s => s.Notes).ReturnsDbSet(notes);
            contextMock.Setup(s => s.Notes.Add(note)).Callback(() => notes.Add(note));

            var repo = new NoteRepository(contextMock.Object);

            //Act
            repo.AddNote(note);


            //Assert
            Assert.Equal(3, notes.Count);
        }
    }
}
