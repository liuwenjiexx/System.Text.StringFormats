using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Windows;
namespace System.Text.StringFormats
{
    public class RefArgFormatProvider : IFormatProvider, ICustomFormatter, IStringFormatter
    {
        static Regex refRegex = new Regex("^\\s*\\$(?<index>\\d)?");

        private ICustomFormatter baseFormatter;
        public RefArgFormatProvider()
        {

        }

        public RefArgFormatProvider(ICustomFormatter baseFormatter)
        {
            this.baseFormatter = baseFormatter;
        }
        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
                return this;
            return null;
        }

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            return Format(format, arg, new object[] { arg }, formatProvider);
        }

        public string Format(string format, object arg, object[] args, IFormatProvider formatProvider)
        {
            string result;
            if (HandleFormat(ref format, arg, args, out result))
            {
                arg = result;
            }

            if (!string.IsNullOrEmpty(format) && baseFormatter != null)
            {
                if (baseFormatter is IStringFormatter sf)
                    return sf.Format(format, arg, args, formatProvider);
                else
                    return baseFormatter.Format(format, arg, formatProvider);
            }

            if (arg is IFormattable)
                return ((IFormattable)arg).ToString(format, CultureInfo.CurrentCulture);
            if (arg != null)
            {
                if (arg is string)
                    return (string)arg;
                else
                    return arg.ToString();
            }

            return string.Empty;
        }
        public bool HandleFormat(ref string format, object arg, object[] args, out string result)
        {
            result = null;
            if (string.IsNullOrEmpty(format))
                return false;
            var m = refRegex.Match(format);
            if (m.Success)
            {
                int? index = null;
                if (m.Groups["index"].Success)
                {
                    if (!int.TryParse(m.Groups["index"].Value, out var i))
                    {
                        throw new FormatException($"{format} format index error");
                    }
                    index = i;
                }

                if (index.HasValue)
                {
                    if (index < 0 || index >= args.Length)
                        throw new FormatException($"{format} ref index error");

                    result = args[index.Value]?.ToString();

                }
                else
                {
                    result = arg?.ToString();
                }
                if (result == null)
                    result = string.Empty;

                format = format.Substring(m.Index + m.Length);
                if (format.StartsWith("."))
                {
                    format = "@" + format.Substring(1);
                }
                return true;
            }

            return false;
        }
    }
}