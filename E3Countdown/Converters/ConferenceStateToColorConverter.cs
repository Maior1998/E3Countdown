using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Media;
using E3Countdown.Model;

namespace E3Countdown.Converters
{
    public class ConferenceStateToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is ConferenceState state)) return new SolidColorBrush(Colors.Black);
            switch (state)
            {
                case ConferenceState.DontStartedYet:
                    return new SolidColorBrush(Colors.Green);
                    break;
                case ConferenceState.Started:
                    return new SolidColorBrush(Colors.Blue);
                    break;
                case ConferenceState.Ended:
                    return new SolidColorBrush(Colors.Red);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
