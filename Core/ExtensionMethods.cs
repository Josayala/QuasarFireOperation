using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public static class ExtensionMethods
    {
        public static bool ConvertToBool(this string ynFlag)
        {
            bool result = "y".Equals(ynFlag, StringComparison.OrdinalIgnoreCase);
            return result;
        }

        public static string ConvertToYesNoString(this bool boolValue)
        {
            string result = boolValue ? "Y" : "N";
            return result;
        }
    }
}
