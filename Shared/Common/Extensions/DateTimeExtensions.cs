using System;

namespace Common.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Get the age from the birthdate.
        /// </summary>
        /// <param name="birthDate">The birthdate from which the age is calculated.</param>
        /// <returns></returns>
        public static int Age(this DateTime birthDate)
        {
            return Age(birthDate, DateTime.Now);
        }

        private static int Age(this DateTime birthDate, DateTime offsetDate)
        {
            int result = 0;
            result = offsetDate.Year - birthDate.Year;

            if (offsetDate.DayOfYear < birthDate.DayOfYear)
            {
                result--;
            }

            return result;
        }
    }
}