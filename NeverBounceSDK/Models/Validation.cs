namespace NeverBounce.Models
{
    class Validation
    {
        public static bool JobIdValidation(int job_id)
        {
            if (job_id <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        public static bool SinglecheckValidation(string email)
        {
            if (email== null || email == "")
            {

                return false;
            }
            else
            {
                if (IsValidEmail(email))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
