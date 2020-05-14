using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using E3Countdown.Model;

namespace E3Countdown
{
    /// <summary>
    /// Interaction logic for EditConferenceWindow.xaml
    /// </summary>
    public partial class EditConferenceWindow : Window
    {
        public EditConferenceWindow()
        {
            InitializeComponent();
        }

        private Conference Target;

        public Conference ShowDialog(Conference source)
        {
            Target = source ?? new Conference("Новая конференция", DateTime.Now, DateTime.Now);
            TbName.Text = Target.Name;
            DtpStart.SelectedDate = Target.StartTime;
            DtpEnd.SelectedDate = Target.EndTime;
            return ShowDialog().Value ? Target : null;
        }


        private void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            if (TbName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Название конференции не может быть пустым!", "Ошибка", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            if (DtpStart.SelectedDate >= DtpEnd.SelectedDate)
            {
                MessageBox.Show("Время начала конференции не может быть позднее времени её конца!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;

            }

            Target.Name = TbName.Text;
            Target.StartTime = DtpStart.SelectedDate;
            Target.EndTime = DtpEnd.SelectedDate;
            DialogResult = true;
            Close();
        }
    }
}
