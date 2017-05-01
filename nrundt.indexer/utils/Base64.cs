using System;
using System.Text;

namespace nrundt.indexer.utils
{
    public static class Base64
    {
        public static string SafeUrlEncode(string text)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(text))
                .TrimEnd('=')
                .Replace('+', '-')
                .Replace('/', '_');
        }
    }
}