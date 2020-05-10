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

namespace E3Countdown.ViewModel
{
    public class E3ViewModel : ReactiveObject
    {
        public E3ViewModel()
        {
            Conferences.Add(new Conference("EA",DateTime.Now, DateTime.Now.AddMinutes(1)));
            Conferences.Add(new Conference("Microsoft", DateTime.Now.AddMinutes(2), DateTime.Now.AddMinutes(3)));
            Conferences.Add(new Conference("Ubisoft", DateTime.Now.AddMinutes(5), DateTime.Now.AddMinutes(6)));
            Conferences.Add(new Conference("Sony", DateTime.Now.AddMinutes(7), DateTime.Now.AddMinutes(8)));

            Observable.Interval(TimeSpan.FromMilliseconds(1000))
                .Subscribe(x =>{ UpdateTime(); });


            Conferences.ToObservableChangeSet()
                .WhenAnyPropertyChanged(nameof(Conference.StartTime), nameof(Conference.EndTime))
                .Subscribe(_ => UpdateTime());
        }
        [Reactive] public ObservableCollection<Conference> Conferences { get; set; } = new ObservableCollection<Conference>();

        private void UpdateTime()
        {
            foreach (Conference conference in Conferences)
            {
                if (DateTime.Now < conference.StartTime)
                {
                    conference.RestTime = conference.StartTime - DateTime.Now;
                }
                else if (DateTime.Now < conference.EndTime)
                {
                    conference.RestTime = conference.EndTime - DateTime.Now;
                }
                else
                {
                    conference.RestTime = DateTime.Now - conference.EndTime;
                }

                //Console.WriteLine($"{conference.Name} - {conference.RestTime}");
            }
        }
    }
}
