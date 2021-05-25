using DonutCP.Model;
using DonutCP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonutCP.Model.DataServices
{
    public static class DataServices 
    {
        public static List<Note> GetAllNotes(int userId)
        {
            using (DonutDataBase db = new DonutDataBase())
            {
                Note _note = db.Note.FirstOrDefault(p => p.Author_Id== userId);
                if(_note!=null && userId == _note.Author_Id)
                {
                    //var result = db.Note.ToList();
                    //var selectedNotes = from _note in result
                    //                    where _note.Author_Id == userId
                    //                    select _note;
                    //foreach (Note _note in selectedNotes)
                    //    result = _note.ToList();

                    var selectedNotes = db.Note.Where(p => p.Author_Id == userId);
                    var result = selectedNotes.ToList();

                    return result;
                }
                
            };
            return new List<Note>();
        }

        public static List<High_Lights> GetAllHightLights(int userId)
        {
            using (DonutDataBase db = new DonutDataBase())
            {
                var result = db.High_Lights.ToList();
                return result;
            }
        }

        public static string CreateUser(string name, string email, string passsword)
        {
            string result = "Пользователь с таким именем уже существует";
            var hasher = new Hasher();
            if (passsword == null || email == null || name == null) return result = "Не заполнено поле";
            var salt = hasher.GetSalt();
            var hash = hasher.Encrypt(passsword, salt);
            using (DonutDataBase db = new DonutDataBase())
            {
                //check the user is exist
                bool checkIsExist = db.Users.Any(el => el.User_Login == name );
                if (!checkIsExist)
                {
                    Users newUser = new Users
                    {
                        User_Login = name,
                        Email = email,
                        PasswordHash = hash,
                        PasswordSalt = salt
                    };
                    db.Users.Add(newUser);
                    db.SaveChanges();
                    result = "Сделано!";
                }
                return result;
            }
        }

        public static string LoginUser(string name, string password)
        {
            string result = "Неверный логин или пароль";
            var hasher = new Hasher();
            if (password == null || name == null) return "Не заполнено поле";
            else if (password == "" || name == "") return "Пустое поле";
            using (DonutDataBase db = new DonutDataBase())
            {
                Users user = db.Users.FirstOrDefault(p => p.User_Login == name);
                if (user != null)
                {
                    var salt = user.PasswordSalt;
                    var hash = user.PasswordHash;
                    var doesHash = hasher.Encrypt(password, salt);
                    if (doesHash != hash) return "Ошибка";
                    return Convert.ToString(user.Id);
                }
            }
            return result;
        }

        public static string EditUser(Users oldUser, string newName, string newEmail, byte[] newImage)
        {
            string result = "Такого сотрудника не существует";
            using (DonutDataBase db = new DonutDataBase())
            {
                //check user is exist
                Users user = db.Users.FirstOrDefault(p => p.Id == oldUser.Id);
                if (user != null)
                {
                    user.User_Login = newName;
                    user.Email = newEmail;
                    user.User_Image = newImage;
                    db.SaveChanges();
                    result = "Сделано! Сотрудник " + user.User_Login  + " изменен";
                }
            }
            return result;
        }

        public static string CreateNote(string name, string description ,int userId)
        {
            using (DonutDataBase db = new DonutDataBase())
            {
                string result = "Создано";
                Note newNote = new Note { Nickname = name,Description_note = description, Author_Id = userId };
                db.Note.Add(newNote);
                db.SaveChanges();
                return result;
            }
        }

        public static string DeleteNote(Note note)
        {
            string result = "Такого отела не существует";
            using (DonutDataBase db = new DonutDataBase())
            {
                db.Entry(note).State = System.Data.Entity.EntityState.Deleted;
                db.Note.Remove(note);
                db.SaveChanges();
                result = "Сделано! Заметка " + note.Nickname + " удалена";
            }
            return result;
        }

        public static string EditNote(Note oldNote, string newName, string newDescription , string access = "" )
        {
            string result = "Такой заметки не существует";
            using (DonutDataBase db = new DonutDataBase())
            {
                //check user is exist

                Note _note = db.Note.FirstOrDefault(el => el.Id == oldNote.Id);
                if(_note != null)
                {
                    if (newName != null && newName != "")
                        _note.Nickname = newName;
                    else if (newDescription != null && newDescription != "")
                        _note.Description_note = newDescription;
                }
                else
                {
                    return result;
                }
                Users user = db.Users.FirstOrDefault(el => el.User_Login == access);
                if (user != null) 
                {
                    NoteAccess _access = new NoteAccess
                    {
                        NoteId = _note.Id,
                        UserId = user.Id
                    };
                    Note newNote = new Note { Nickname = _note.Nickname, Description_note = _note.Description_note, Author_Id = user.Id };
                    db.Note.Add(newNote);
                }
                else
                {
                    result = "Такого пользователя нет";
                    return result;
                }

                
                db.SaveChanges();
                result = "Изменено! Записка " + _note.Nickname + "изменена";
                return result;   
            }
        }

        public static string CreateHightLight(int noteId, int startInex, int lenght, string text, int userId)
        {
            string result = "Уже существует";
            using (DonutDataBase db = new DonutDataBase())
            {
                //check the user is exist
                bool checkIsExist = db.High_Lights.Any(el => el.Note_Id == noteId && el.Start_Text_Index == startInex && el.Length_Hight_Light == lenght && el.Text_Hight_Light == text && el.Author_Id == userId);
                if (!checkIsExist)
                {
                    High_Lights newHightLight = new High_Lights
                    {
                        Note_Id = noteId,
                        Start_Text_Index = startInex,
                        Length_Hight_Light = lenght,
                        Text_Hight_Light = text,
                        Author_Id = userId
                    };
                    db.High_Lights.Add(newHightLight);
                    db.SaveChanges();
                    result = "Сделано!";
                }
                return result;
            }
        }

        public static string DeleteHightLight(High_Lights hightLight)
        {
            string result = "Такого отела не существует";
            using (DonutDataBase db = new DonutDataBase())
            {
                db.Entry(hightLight).State = System.Data.Entity.EntityState.Deleted;
                db.High_Lights.Remove(hightLight);
                db.SaveChanges();
                result = "Сделано! Выноска удалена";
            }
            return result;
        }

    }
}
