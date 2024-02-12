using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using System.Globalization;

namespace DevWorld.LaContessa.Command.Abstractions.Utilities
{
    public class DateValidator
    {
        public static DateTime? Validate(string? dateToValidate)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(dateToValidate)) return null;

                return DateTime.ParseExact(dateToValidate, "dd MMM yyyy", CultureInfo.CreateSpecificCulture("it-IT"));
            }
            catch (Exception)
            {
                throw new DateValidationException();
            }
        }
    }
}
