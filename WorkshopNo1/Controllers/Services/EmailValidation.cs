using System.Text.RegularExpressions;

namespace WorkshopNo1.Controllers.Services;

    public abstract class EmailValidation
    {
        public static bool IsValid(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            try
            {
                var pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$"; 
                var regex = new Regex(pattern);
                return regex.IsMatch(email);
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

    }