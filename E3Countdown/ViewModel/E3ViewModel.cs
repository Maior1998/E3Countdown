using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DynamicData;
using DynamicData.Binding;
using E3Countdown.Model;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using E3Countdown.Commands;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;

namespace E3Countdown.ViewModel
{
    public class E3ViewModel : ReactiveObject
    {
        public E3ViewModel()
        {
            Conferences.Add(new Conference("EA", DateTime.Now, DateTime.Now.AddMinutes(1)));
            Conferences.Add(new Conference("Microsoft", DateTime.Now.AddMinutes(2), DateTime.Now.AddMinutes(3)));
            Conferences.Add(new Conference("Ubisoft", DateTime.Now.AddMinutes(5), DateTime.Now.AddMinutes(6)));
            Conferences.Add(new Conference("Sony", DateTime.Now.AddMinutes(7), DateTime.Now.AddMinutes(8)));
        }
        [Reactive] public ObservableCollection<Conference> Conferences { get; set; } = new ObservableCollection<Conference>();

        [Reactive] public Conference SelectedConference { get; set; }

        
        public VMCommand RemoveConference => new VMCommand(
            (obj) =>
            {
                if (!(obj is Conference)) return;
                Conferences.Remove((Conference)obj);
            }, _ => !(SelectedConference is null));

        public VMCommand AddConference => new VMCommand((obj) =>
        {
            EditConferenceWindow editConferenceWindow = new EditConferenceWindow();
            Conference result = editConferenceWindow.ShowDialog(null);
            if (result is null) return;
            Conferences.Add(result);
        });

        public VMCommand EditConference => new VMCommand((obj) =>
        {
            if (!(obj is Conference source)) return;
            EditConferenceWindow editConferenceWindow = new EditConferenceWindow();
            Conference result = editConferenceWindow.ShowDialog(source);
            if (result is null) return;
            source.Name = result.Name;
            source.StartTime = result.StartTime;
            source.EndTime = result.EndTime;
            
        }, _ => !(SelectedConference is null));

        

        public VMCommand Load => new VMCommand((obj) =>
        {
            OpenFileDialog opening = new OpenFileDialog();
            if (!opening.ShowDialog().Value) return;
            string[] source = File.ReadAllLines(opening.FileName, Encoding.UTF8);
            ObservableCollection<Conference> result = new ObservableCollection<Conference>();
            foreach (string stringConference in source)
                result.Add(new Conference(stringConference));
            Conferences = result;
        });

        public VMCommand Save => new VMCommand((obj) =>
        {
            //EA 01.01.2020 10:23 - 02.02.2020 12:34 
            SaveFileDialog saving = new SaveFileDialog();
            if (!saving.ShowDialog().Value) return;
            File.WriteAllLines(saving.FileName,Conferences.Select(conf=>$"{conf.Name} {conf.StartTime:dd.MM.yyyy HH:mm} - {conf.EndTime:dd.MM.yyyy HH:mm}"),Encoding.UTF8);
        },_=>Conferences.Count!=0);
    }
}
