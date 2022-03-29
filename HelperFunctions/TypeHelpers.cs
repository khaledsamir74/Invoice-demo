using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceDemo.HelperFunctions
{
    public class TypeHelpers
    {
        static public bool IsAlphabeticString(object value)
        {
            string str = value as string;
            return str != null && IsAllAlphabetic(str);
        }
        static public bool IsAllAlphabetic(string value)
        {
            foreach (char c in value)
            {
                if (!char.IsLetter(c))
                    return false;
            }
            return true;
        }
        static public bool IsInteger(string n)
        {
            int i;
            return int.TryParse(n, out i);
        }
        static public bool IsFloat(string n)
        {
            decimal i;
            return decimal.TryParse(n, out i) && n is not null;
        }
        static public bool IsDate(string date)
        {
            DateTime temp;
            if (DateTime.TryParse(date, out temp))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        static public bool IsNullOrEmpty(string x)
        {
            return String.IsNullOrEmpty(x);
        }

    }
}
