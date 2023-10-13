using System;
using Rivel.Framework;

namespace Rivel.Services
{
    public class DateService
    {
        public static DateTime CurrentDate => DateTime.Now;


        /// <summary>
        /// Calculates a new date by adding the specified number of days to the given date.
        /// </summary>
        /// <param name="date">The date to add days to.</param>
        /// <param name="days">The number of days to add to the date. Defaults to 0.</param>
        /// <returns>The resulting date.</returns>
        public static string CalcDate(DateTime date, int days = 0)
        {
            return date.AddDays(days).ToMySqlDate();
        }

    }
}