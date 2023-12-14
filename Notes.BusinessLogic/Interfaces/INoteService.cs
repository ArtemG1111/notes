﻿

using Notes.DataAccess.Data.Models;

namespace Notes.BusinessLogic.Interfaces
{
    public interface INoteService
    {
        void AddNote(Note note);
        void UpdateNote(Note note);
        void DeleteNote(Note note);
        List<Note> GetAllNote();
        Note GetNoteById(int id);

    }
}
