using DonutCP.Model;
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
                var result = db.Note.ToList();
                return result;
            }
        }

        public static List<High_Lights> GetAllHightLights()
        {
            using (DonutDataBase db = new DonutDataBase())
            {
                var result = db.High_Lights.ToList();
                return result;
            }
        }

        public static string CreateUser(string name, string email, byte[] passsword)
        {
            string result = "Уже существует";
            using (DonutDataBase db = new DonutDataBase())
            {
                //check the user is exist
                bool checkIsExist = db.Users.Any(el => el.User_Login == name && el.Email == email && el.PasswordHash == passsword);
                if (!checkIsExist)
                {
                    Users newUser = new Users
                    {
                        User_Login = name,
                        Email = email,
                        PasswordHash = passsword
                    };
                    db.Users.Add(newUser);
                    db.SaveChanges();
                    result = "Сделано!";
                }
                return result;
            }
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

        public static string CreateNote(int userId)
        {
            using (DonutDataBase db = new DonutDataBase())
            {
                string result = "Создано";
                string name = "Заметка";
                Note newNote = new Note { Nickname = name, Author_Id = userId };
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
                db.Note.Remove(note);
                db.SaveChanges();
                result = "Сделано! Записка " + note.Nickname + " удалена";
            }
            return result;
        }

        public static string EditNote(Note oldNote, string newName, string newDescription, string newText)
        {
            string result = "Такого сотрудника не существует";
            using (DonutDataBase db = new DonutDataBase())
            {
                //check user is exist
                Note _note = db.Note.FirstOrDefault(el => el.Id == oldNote.Id);
                if(_note != null)
                {
                    _note.Nickname = newName;
                    _note.Description_note = newDescription;
                    _note.Text_note = newText;
                    db.SaveChanges();
                    result = "Изменено! Записка " + _note.Nickname + "изменена";
                }
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
                db.High_Lights.Remove(hightLight);
                db.SaveChanges();
                result = "Сделано! Выноска удалена";
            }
            return result;
        }



    }
}
