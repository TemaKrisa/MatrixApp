using System.Linq;

namespace Matrix.Class
{
    /// <summary>
    /// Данный класс проверяет данные указанные при регистрации
    /// </summary>
    public static class RegistrationClass
    {
        public static string RegCheck(string Surname, string Name, string Email, string Password)
        {
            bool SurnameisDigit = false;
            bool NameIsDigit = false;
            if (Surname.Any(char.IsDigit)) SurnameisDigit = true; //Проверка фамилии на содержание цифер
            if (Name.Any(char.IsDigit)) NameIsDigit = true; //Проверка имени на содержание цифер
            if (string.IsNullOrWhiteSpace(Surname)) return "EmptySurname"; //Проверка фамилии на наличие
            else if (Surname.Length > 25) return "InvalidSurname"; //Проверка фамилии на длинну
            else if (string.IsNullOrWhiteSpace(Name)) return "EmptyName"; //Проверка имени на наличие
            else if (Name.Length > 25) return "InvalidName"; //Проверка имени на длинну
            else if (string.IsNullOrWhiteSpace(Email)) return "EmptyEmail"; //Проверка почты на наличие
            else if (string.IsNullOrWhiteSpace(Password)) return "EmptyPassword"; //Проверка пароля на наличие
            else if (Password.Length > 25) return "InvalidPassword"; //проверка пароля на длинну
            else if (SurnameisDigit) return "SurnameIsDigit"; 
            else if (NameIsDigit) return "NameIsDigit"; 
            else return "Ok";
        }
    }
}
