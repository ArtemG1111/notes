

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;
using Notes.BusinessLogic.Servises;
using Notes.DataAccess.Data.Models;
using Notes.DataAccess.Interfaces;

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
            mock.Setup(r=>r.GetNoteById(1)).Returns(note);
            var cacheMock = new Mock<IMemoryCache>();
            var loggerMock = new Mock<ILogger<NoteService>>();
            var service = new NoteService(mock.Object, cacheMock.Object, loggerMock.Object);

            //Act
            var result = service.GetNoteById(1);

            //Assert
            Assert.Equal(note, result);
        }
    }
}
