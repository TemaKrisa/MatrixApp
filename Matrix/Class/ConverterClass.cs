using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace Matrix.Class
{
    public static class ConverterClass
    {
        public static decimal PhoneConverter(string Phone)
        {
            string formattedPhoneNumber = Regex.Replace(Phone, @"[^0-9+]", ""); // удаление всех символов, кроме цифр и "+"
            formattedPhoneNumber = formattedPhoneNumber.Replace("+", "8"); // замена + на 8
            return decimal.Parse(formattedPhoneNumber); 
        }
    }
}
