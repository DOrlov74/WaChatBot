using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WaChatBot.Services
{
  public static class Utils
  {
    public static bool IsPhoneNbr(string? number)
    {
      const string pattern = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{2})[-. ]?([0-9]{2})$";
      if (number != null) return Regex.IsMatch(number, pattern);
      else return false;
    }

    public static string CleanPhoneNumber(string? number)
    {
      if (number != null) return Regex.Replace(number, @"[^\d]", "");
      else return "";
    }
  }
}
