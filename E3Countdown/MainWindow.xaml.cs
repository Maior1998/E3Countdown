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
using System.Windows.Navigation;
using System.Windows.Shapes;
using E3Countdown.Model;
using E3Countdown.ViewModel;

namespace E3Countdown
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private Conference test;
        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            test = new Conference("EA",new DateTime(2020,05,05,20,20,20), new DateTime(2020, 05, 05, 22, 20, 20));
            E3ViewModel testModel = new E3ViewModel();
            testModel.Conferences.Add(test);
            test = new Conference("AE", new DateTime(2020, 05, 05, 21, 20, 20), new DateTime(2020, 05, 05, 22, 20, 20));
            testModel.Conferences.Add(test);
        }
    }
}
