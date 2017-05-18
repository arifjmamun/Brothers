using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helper
{
    public static class IntExtensions
    {
        public static string EmptyIfZero(this int value)
        {
            if (value == 0)
                return string.Empty;

            return value.ToString();
        }

        public static string ToEmptyString(this int value)
        {
            return value == 0 ? string.Empty : value.ToString();
        }
    }
}
