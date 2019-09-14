using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extension
{
    public static class StringExtension
    {
        public static Int16 ToInt16(this string s)
        {
            if (s == null || s.Equals("") || s.Equals(string.Empty)) return 0;
            return Int16.Parse(s.Trim());
        }
        public static short Toshort(this string s)
        {
            return s.ToInt16();
        }
        public static Int32 ToInt32(this string s)
        {
            if (s == null || s.Equals("") || s.Equals(string.Empty)) return 0;
            return Int32.Parse(s.Trim());
        }
        public static int Toint(this string s)
        {
            return s.ToInt32();
        }
        public static Int64 ToInt64(this string s)
        {
            if (s == null || s.Equals("") || s.Equals(string.Empty)) return 0;
            return Int64.Parse(s.Trim());
        }
        public static long Tolong(this string s)
        {
            return s.ToInt64();
        }
        public static Decimal ToDecimal(this string s)
        {
            if (s == null || s.Equals("") || s.Equals(string.Empty)) return 0.0m;
            return Decimal.Parse(s.Trim());
        }
        public static decimal Todecimal(this string s)
        {
            return s.ToDecimal();
        }
        public static Double ToDouble(this string s)
        {
            if (s == null || s.Equals("") || s.Equals(string.Empty)) return 0.0d;
            return Double.Parse(s.Trim());
        }
        public static Single ToSingle(this string s)
        {
            if (s == null || s.Equals("") || s.Equals(string.Empty)) return 0.0f;
            return Single.Parse(s.Trim());
        }
        public static float ToFloat(this string s)
        {
            return s.ToSingle();
        }
        public static DateTime ToDateTime(this string s)
        {
            if (s == null || s.Equals("") || s.Equals(string.Empty)) return DateTime.Now;
            return DateTime.Parse(s.Trim());
        }
        public static double Todouble(this string s)
        {
            return s.ToDouble();
        }
    }
}
