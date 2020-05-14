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
using E3Countdown.Model.ConferenceStates;

namespace E3Countdown.Converters
{
    public class ConferenceStateToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is ConferenceState state)) return new SolidColorBrush(Colors.Black);
            switch (state)
            {
                case DontStartedConferenceState _:
                    return new SolidColorBrush(Colors.DarkGreen);
                case StartedConferenceState _:
                    return new SolidColorBrush(Colors.CornflowerBlue);
                case EndedConferenceState _:
                    return new SolidColorBrush(Colors.DarkGray);
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
