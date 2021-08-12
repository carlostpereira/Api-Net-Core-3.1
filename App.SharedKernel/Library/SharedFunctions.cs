using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace App.SharedKernel.Library
{
    public static class SharedFunctions
    {

        public static string RemoveAccents(string text)
        {
            try
            {
                text = text.Normalize(NormalizationForm.FormD);
                StringBuilder sb = new StringBuilder();

                foreach (char c in text.ToCharArray())
                {
                    if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                        sb.Append(c);
                }
                return sb.ToString();
            }
            catch { return ""; }
        }

        /*
            Função para remover letras do alfabeto a partir de um GUID (Identificador Global Unico)
            Autor: Rogerio
            Data: 06/2013
        */
        public static string ExtractNumbers(string value, int len)
        {
            string[] textChars = new string[]{
            "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S",
            "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l",
            "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"};

            value = RemoveAccents(value);
            value = value.Replace("-", string.Empty);
            foreach (string itemChar in textChars)
            {
                value = value.Replace(itemChar, string.Empty);
            }

            if (len <= 0)
            {
                len = 2;
            }

            if (value.Length > len)
                value = value.Substring(0, len);

            return value;
        }

        public static bool IsNumeric(Object Expression)
        {
            if (Expression == null || Expression is DateTime)
                return false;

            if (Expression is Int16 || Expression is Int32 || Expression is Int64 ||
               Expression is Decimal || Expression is Single || Expression is Double ||
               Expression is Boolean)
                return true;

            try
            {
                if (Expression is string)
                    Double.Parse(Expression as string);
                else
                    Double.Parse(Expression.ToString());

                return true;
            }
            catch { }
            return false;
        }

        public static bool IsValidEmail(string email)
        {
            email = email.Trim();
            bool result = false;

            if (string.IsNullOrEmpty(email))
                result = true;
            else
            {
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(email);
                result = match.Success;
            }

            return result;
        }


        //Datetime returns
        public static DateTime GetDate()
        {
            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        }

        public static DateTime FirstDateOfMonth()
        {
            return new DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, 1);
        }

        public static DateTime FirstDateOfMonth(int month)
        {
            return new DateTime(System.DateTime.Now.Year, month, 1);
        }

        public static DateTime FirstDateOfMonth(int year, int month)
        {
            return new DateTime(year, month, 1);
        }

        public static DateTime LastDateOfMonth()
        {
            return new DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month,
                DateTime.DaysInMonth(System.DateTime.Now.Year, System.DateTime.Now.Month));
        }

        public static DateTime LastDateOfMonth(int month)
        {
            return new DateTime(System.DateTime.Now.Year, month, DateTime.DaysInMonth(System.DateTime.Now.Year, month));
        }

        public static DateTime LastDateOfMonth(int month, int year)
        {
            return new DateTime(year, month, DateTime.DaysInMonth(year, month));
        }

        public static int DaysBetweenTwoIntervalDates(DateTime dataInicial, DateTime dataFinal)
        {
            return (int)dataInicial.Subtract(dataFinal).TotalDays;
        }

        #region Laboratorial Exams

        public static ICollection<decimal[]> SetQuartil(decimal minValue, decimal maxValue)
        {
            decimal minValueQuartil = 0;
            decimal maxValueQuartil = 0;
            decimal constQuartil = 0;
            ICollection<decimal[]> quartil = new List<decimal[]>();

            constQuartil = ((maxValue - minValue) / 4);

            for (int item = 0; item < 4; item++)
            {
                if (maxValueQuartil.Equals(0))
                {
                    minValueQuartil = minValue;
                    maxValueQuartil = minValue + constQuartil;
                }
                else
                {
                    minValueQuartil = maxValueQuartil;
                    maxValueQuartil = minValueQuartil + constQuartil;
                }

                quartil.Add(new decimal[] { minValueQuartil, maxValueQuartil });
            }

            return quartil;
        }

        public static int GetQuartil(decimal valueToCheck, ICollection<decimal[]> valueArray)
        {

            if (valueToCheck >= valueArray.ToList()[0][0] && valueToCheck <= valueArray.ToList()[0][1])
                return 1;

            if (valueToCheck >= valueArray.ToList()[1][0] && valueToCheck <= valueArray.ToList()[1][1])
                return 2;

            if (valueToCheck >= valueArray.ToList()[2][0] && valueToCheck <= valueArray.ToList()[2][1])
                return 3;

            if (valueToCheck >= valueArray.ToList()[3][0] && valueToCheck <= valueArray.ToList()[3][1])
                return 4;

            return -1;
        }

        public static string GetImc(double imc)
        {
            switch (imc)
            {
                case double n when (n < 18.5):
                    return "Magreza";

                case double n when (n >= 18.6 && n <= 24.9):
                    return "Normal";

                case double n when (n >= 25.0 && n <= 29.9):
                    return "Sobrepeso";

                case double n when (n >= 30.0 && n <= 34.9):
                    return "Obesidade Grau I";

                case double n when (n >= 35.0 && n <= 39.9):
                    return "Obesidade Grau II";

                case double n when (n >= 40):
                    return "Obesidade Grau III";
            }

            return "Indefinido";

        }

        #endregion

        #region Enumerators

        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }

        #endregion

        #region Encrypting

        public static string EncryptPassword(string pass)
        {
            return EncryptFunctions.EncryptPassword(pass);
        }

        #endregion




    }
}
