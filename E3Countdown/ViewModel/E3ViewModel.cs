using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicData;
using DynamicData.Binding;
using E3Countdown.Model;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using E3Countdown.Commands;

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
    }
}
