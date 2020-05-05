using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicData.Binding;
using E3Countdown.Model;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace E3Countdown.ViewModel
{
    public class E3ViewModel : ReactiveObject
    {
        public E3ViewModel()
        {
            Observable.Interval(TimeSpan.FromMilliseconds(1000))
                .Subscribe(x =>
                {
                    foreach (Conference conference in Conferences)
                    {
                        if (DateTime.Now < conference.StartTime)
                            conference.RestTime = conference.StartTime - DateTime.Now;
                        else if (DateTime.Now < conference.EndTime)
                            conference.RestTime = conference.EndTime - DateTime.Now;
                        else
                            conference.RestTime = DateTime.Now - conference.EndTime;
                        Console.WriteLine($"{conference.Name} - {conference.RestTime}");
                    }
                    
                });
        }
        [Reactive] public ObservableCollection<Conference> Conferences { get; set; } = new ObservableCollection<Conference>();



    }
}
