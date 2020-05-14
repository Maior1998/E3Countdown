using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace E3Countdown.Converters
{
    public class TimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is TimeSpan source)) return "Wrong type.";
            int hours = (int)source.TotalHours;
            int minutes = source.Minutes;
            int seconds = source.Seconds;
            List<string> parts = new List<string>();

            if (hours > 0)
                parts.Add(GetWordCase(hours, "час", "часа", "часов"));
            if (minutes > 0)
                parts.Add(GetWordCase(minutes, "минута", "минуты", "минут"));
            if (seconds > 0)
                parts.Add(GetWordCase(seconds, "секунда", "секунды", "секунд"));
            return string.Join(", ", parts);
        }

        /// <summary>
        /// Возвращает нужный падеж слова с указанным числом. 21 час, 11 часов, 2 часа и т.д.
        /// </summary>
        /// <param name="num"></param>
        /// <param name="case1">Строковое представление числа с единицей на конце, кроме 10-19 (1 час, 31 час, ...)</param>
        /// <param name="case2">Строковое представление числа с 2-4 на конце, кроме 10-19 (2 часа, 23 часа)</param>
        /// <param name="case3">Строковое представление числа с 5-9 на конце, кроме 10-19 (5 часов, 25 часов)</param>
        /// <returns>23 часа</returns>
        private string GetWordCase(int num, string case1, string case2, string case3)
        {
            int lastdigit = num % 10;
            int prevdigit = num % 100 / 10;
            if (prevdigit == 1 || lastdigit > 4 || lastdigit == 0) return $"{num} {case3}";
            if (lastdigit == 1) return $"{num} {case1}";
            return $"{num} {case2}";

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
