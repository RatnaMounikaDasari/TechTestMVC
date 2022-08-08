using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace TechTestMVC.Formaters
{
    public class PhoneNumberFormater
    {
        /// <summary>
        /// Phone Number check
        /// </summary>
        /// <returns>phone number or Invalid or Missed</returns>
        public  string isValidMobileNumber(string inputMobileNumber, string state, out bool isValid)
        {
            var mobileNumber = "";
            try
            {
                if (!string.IsNullOrEmpty(inputMobileNumber))
                {
                    var result = new string(inputMobileNumber.Where(c => char.IsDigit(c)).ToArray());
                    if (result.Length < 8 || result.Length > 10)
                    {
                        isValid = false;
                        return "Invalid";
                    }
                    else
                    {
                        string regExp = result.Length == 8 ? @"^\d{8}$" : @"^(?=.*)((?:\+61) ?(?:\((?=.*\)))?([2-47-8])\)?|(?:\((?=.*\)))?([0-1][2-47-8])\)?) ?-?(?=.*)((\d{1} ?-?\d{3}$)|(00 ?-?\d{4} ?-?\d{4}$)|( ?-?\d{4} ?-?\d{4}$)|(\d{2} ?-?\d{3} ?-?\d{3}$))";
                        Regex re = new Regex(regExp);
                        if (re.IsMatch(result))
                        {
                            switch (state)
                            {
                                case "NSW":
                                    mobileNumber = result.Length == 8 ? "(02) " + result.Insert(4, " ") : result.Insert(0, "(").Insert(3, ") ").Insert(9, " ");
                                    break;
                                case "QLD":
                                    mobileNumber = result.Length == 8 ? "(07) " + result.Insert(4, " ") : result.Insert(0, "(").Insert(3, ") ").Insert(9, " ");
                                    break;
                                case "VIC":
                                    mobileNumber = result.Length == 8 ? "(03) " + result.Insert(4, " ") : result.Insert(0, "(").Insert(3, ") ").Insert(9, " ");
                                    break;
                                case "WA":
                                    mobileNumber = result.Length == 8 ? "(08) " + result.Insert(4, " ") : result.Insert(0, "(").Insert(3, ") ").Insert(9, " ");
                                    break;
                                default:
                                    mobileNumber = result.Length == 8 ? "(02) " + result.Insert(4, " ") : result.Insert(0, "(").Insert(3, ") ").Insert(9, " ");
                                    break;
                            }
                            isValid = true;
                            return mobileNumber;
                        }
                        else
                        {
                            isValid = false;
                            return "Invalid";
                        }
                    }
                }
                else
                {
                    isValid = false;
                    return "Missed";
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
