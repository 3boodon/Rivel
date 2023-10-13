using System;
using System.Text.RegularExpressions;

namespace Rivel.Framework
{
    internal static class StringExtensions
    {
        /// <summary>
        /// Determines whether the specified string is a valid password based on the following criteria:<br/>
        /// - Contains at least 1 lower case character <br/>
        /// - Contains at least 1 upper case character <br/>
        /// - Contains at least 1 digit <br/>
        /// - Has a length of 6 or more characters <br/>
        /// </summary>
        /// <param name="input">The string to validate as a password.</param>
        /// <returns>true if the input string is a valid password; otherwise, false.</returns>
        public static bool IsValidPassword(this string input)
        {
            Regex requiredLength = new Regex("^.{6,}$"); // Matches 6 Characters or more
            Regex lowerCase = new Regex("^.*(?=.*[a-z])[a-z]{1,}.*$"); // Matches 1 or more lower case characters 
            Regex upperCase = new Regex("^.*(?=.*[A-Z])[A-Z]{1,}.*$"); // Matches 1 or more upper case characters 
            Regex digits = new Regex("^.*(?=.*\\d)[\\d]{1,}.*$"); // Matches 1 or more digits 

            try
            {
                if (!lowerCase.IsMatch(input))
                    throw new Exception("Password must contain at least 1 lower case character");
                if (!upperCase.IsMatch(input))
                    throw new Exception("Password must contain at least 1 upper case character");
                if (!digits.IsMatch(input))
                    throw new Exception("Password must contain at least 1 digit");
                if (!requiredLength.IsMatch(input))
                    throw new FormatException("Password length Must be 6 or more");
                return true;
            }
            catch (Exception e)
            {
                Helper.Print(e.Message, MessageType.ERROR);
                return false;
            }
        }



        /// <summary>
        /// Converts a string to snake case.
        /// </summary>
        /// <param name="str">The string to convert.</param>
        /// <returns>The snake case version of the input string.</returns>
        public static string ToSnakeCase(this string str)
        {
            string snakeCase = "";
            foreach (char c in str)
            {
                // the first character should be lowercase without an underscore
                if (snakeCase.Length == 0)
                {
                    snakeCase += char.ToLower(c);
                }
                else if (char.IsUpper(c))
                {
                    snakeCase += $"_{char.ToLower(c)}";
                }
                else
                {
                    snakeCase += c;
                }
            }
            return snakeCase;
        }


        /// <summary>
        /// Fills the string with the specified filler character until it reaches the specified length.
        /// </summary>
        /// <param name="str">The string to fill.</param>
        /// <param name="length">The desired length of the string.</param>
        /// <param name="filler">The character to use for filling. Default is space.</param>
        /// <returns>The filled string.</returns>
        public static string Fill(this string str, int length, char filler = ' ')
        {
            if (str.Length < length)
            {
                int neededSpaces = length - str.Length;
                for (int i = 0; i < neededSpaces; i++)
                {
                    str += filler;
                }
            }
            return str;
        }


        /// <summary>
        /// Converts the specified decimal number to a string representation of a currency value, using the specified currency symbol.
        /// </summary>
        /// <param name="number">The decimal number to convert.</param>
        /// <param name="currencySymbol">The currency symbol to use. The default value is '$'.</param>
        /// <returns>A string representation of the currency value.</returns>
        public static string AsCurrency(this decimal number, char currencySymbol = '$')
        {
            return $"{number} {currencySymbol}";
        }



        /// <summary>
        /// Determines whether the specified string is a valid date in the format yyyy-mm-dd.
        /// </summary>
        /// <param name="date">The string to validate.</param>
        /// <returns>true if the string is a valid date in the format yyyy-mm-dd; otherwise, false.</returns>
        public static bool IsValidDate(this string date)
        {
            Regex dateFormat = new Regex("^\\d{4}-\\d{2}-\\d{2}$");
            try
            {
                if (!dateFormat.IsMatch(date))
                {
                    throw new Exception("Date format must be yyyy-mm-dd");
                }
                return true;
            }
            catch (Exception e)
            {
                Helper.Print(e.Message, MessageType.ERROR);
                return false;
            }
        }


        /// <summary>
        /// Creates a DateTime Object from a string.
        /// </summary>
        /// <param name="date">The string to convert.</param>
        /// <returns>A DateTime object representing the specified date.</returns>
        public static string ToMySqlDate(this DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }


        /// <summary>
        /// Determines whether the specified date is in the past.
        /// </summary>
        /// <param name="date">The date to check.</param>
        /// <returns>true if the specified date is in the past; otherwise, false.</returns>
        public static bool IsPast(this DateTime date)
        {
            return date < DateTime.Now;
        }



    }
}
