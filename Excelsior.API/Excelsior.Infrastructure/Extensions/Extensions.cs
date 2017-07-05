using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Excelsior.Infrastructure.Extensions
{
    public static partial class CommonExtensions
    {
        public static bool IsNull(this object target)
        {
            return target == null;
        }

        public static bool IsNotNull(this object target)
        {
            return target != null;
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsNotNullOrEmpty(this string value)
        {
            return !value.IsNullOrEmpty();
        }

        public static bool ToggleValue(this bool value)
        {
            return !value;
        }

        public static double ToNumber(this bool value)
        {
            return value ? 1.0d : 0.0d;
        }

        [DebuggerStepThrough]
        public static void Fire(this EventHandler handler, object sender)
        {
            handler?.Invoke(sender, EventArgs.Empty);
        }

        public static string ConcatStringsWithDelimiter(string source, string input, string delimiter)
        {
            if (String.IsNullOrWhiteSpace(source))
                return input;
            else
            {
                if (String.IsNullOrWhiteSpace(input))
                    return source;
                else
                    return source + delimiter + input;
            }
        }

        public static T Clone<T>(this T source, string[] propertiesToIgnore = null) where T : class, new()
        {
            T cloned = new T();

            foreach (var pi in source.GetType().GetProperties())
            {
                var property = cloned.GetType().GetProperty(pi.Name);

                if (property != null && (propertiesToIgnore == null || !propertiesToIgnore.Contains(pi.Name)))
                {
                    var value = pi.GetValue(source, null);

                    if (value != null && property.CanWrite)
                        property.SetValue(cloned, value, null);
                }
            }

            return cloned;
        }

        public static IEnumerable<T> CloneEnumerable<T>(this IEnumerable list) where T : class
        {
            foreach (var item in list)
            {
                if (item is T)
                    yield return (T)item.Clone();
            }
        }
    }

    public static class CollectionExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach (T item in list)
                action(item);
        }

        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action, Func<T, bool> when)
        {
            foreach (T item in list)
            {
                if (when(item))
                {
                    action(item);
                }
            }
        }

        public static void ForEach<T>(this IList<T> list, Action<int, T> action)
        {
            for (int i = 0; i < list.Count; i++)
                action(i, list[i]);
        }

        public static IEnumerable<T> Filter<T>(this IEnumerable list) where T : class
        {
            foreach (var item in list)
            {
                if (item is T)
                    yield return (T)item;
            }
        }

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> list)
        {
            return new ObservableCollection<T>(list);
        }

        public static bool IsNotEmpty<T>(this IEnumerable<T> list)
        {
            return list.Count() != 0;
        }

        public static bool IsEmpty<T>(this IEnumerable<T> list)
        {
            return list.Count() == 0;
        }
    }

    public static class EmailExtensions
    {
        public static bool IsValidEmail(this string strIn)
        {
            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(strIn,
                    @"^(?("")(""[^""]+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
        }
    }

    public static class CountExtensions
    {
        public static bool IsOne(this int value)
        {
            return value == 1;
        }

        public static bool IsGreaterThanOne(this int value)
        {
            return value > 1;
        }
    }

    public static class MathExtensions
    {
        public static double Squared(this double val)
        {
            return val * val;
        }

        public static double SquareRoot(this double val)
        {
            return Math.Sqrt(val);
        }

        public static double Abs(this double val)
        {
            return Math.Abs(val);
        }

        public static int Round(this double val)
        {
            return (int)Math.Round(val);
        }
    }

    public static class EnumExtensions
    {
        public static T[] GetValues<T>()
        {
            Type enumType = typeof(T);

            if (!enumType.IsEnum)
            {
                throw new ArgumentException("Type '" + enumType.Name + "' is not an enum");
            }

            List<T> values = new List<T>();

            var fields = from field in enumType.GetFields()
                         where field.IsLiteral
                         select field;

            foreach (FieldInfo field in fields)
            {
                object value = field.GetValue(enumType);
                values.Add((T)value);
            }

            return values.ToArray();
        }

        public static object[] GetValues(Type enumType)
        {
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("Type '" + enumType.Name + "' is not an enum");
            }

            List<object> values = new List<object>();

            var fields = enumType.GetFields().Where(f => f.IsLiteral);

            foreach (FieldInfo field in fields)
            {
                object value = field.GetValue(enumType);
                values.Add(value);
            }

            return values.ToArray();
        }

        public static string GetDescription(this object value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    DescriptionAttribute attr =
                           Attribute.GetCustomAttribute(field,
                             typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }
            return null;
        }

        public static string[] GetDescriptions(Type enumType)
        {
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("Type '" + enumType.Name + "' is not an enum");
            }

            List<string> values = new List<string>();

            var fields = enumType.GetFields().Where(f => f.IsLiteral);

            foreach (FieldInfo field in fields)
            {
                object value = field.GetValue(enumType);
                values.Add(value.GetDescription());
            }

            return values.ToArray();
        }

        public static object GetValueFromDescription(Type enumType, string description)
        {
            var fields = enumType.GetFields().Where(f => f.IsLiteral);

            return fields.First(f => f.GetValue(enumType).GetDescription() == description).GetValue(enumType);
        }

        public static object GetValueFromStringValue(Type enumType, string sValue)
        {
            var fields = enumType.GetFields().Where(f => f.IsLiteral);

            return fields.First(f => f.GetValue(enumType).ToString() == sValue).GetValue(enumType);
        }
    }
}
