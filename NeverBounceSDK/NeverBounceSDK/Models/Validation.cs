using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverBounce.Models
{
     class Validation
    {
        public static bool JobCreateValidation(JobCreateRequestModel model)
        {
            if (model.input_location.ToLower() != "remote_url" && model.input_location.ToLower() != "supplied")
            {
                return false;
            }
            if (model.auto_parse <0)
            {
                return false;
            }
            if (model.auto_run <0)
            {
                return false;
            }
            if (model.input.Count <= 0 || model.input == null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public static bool JobParseValidation(JobParseRequestModel model)
        {
            if (model.job_id <= 0)
            {
                return false;
            }
            if (model.auto_start <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

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
