using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DynamicData.Binding;
using E3Countdown.Model.ConferenceStates;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace E3Countdown.Model
{
    public class Conference : ReactiveObject
    {
        //EA 01.01.2020 10:23 - 02.02.2020 12:34 
        private static readonly Regex conferenceRegex = new Regex(
            @"(?<name>.+)\s+(?<startday>\d{2})\.(?<startmounth>\d{2})\.(?<startyear>\d{4})\s+(?<starthour>\d{2}):(?<startminute>\d{2})\s+\-\s+(?<endday>\d{2})\.(?<endmounth>\d{2})\.(?<endyear>\d{4})\s+(?<endhour>\d{2}):(?<endminute>\d{2})");

        public Conference()
        {
            dontStarted = new DontStartedConferenceState(this);
            started = new StartedConferenceState(this);
            ended = new EndedConferenceState(this);
            UpdateState();
            Observable.Interval(TimeSpan.FromMilliseconds(1000)).Subscribe(_ => { UpdateState(); });
            this.WhenAnyValue(x => x.StartTime, x => x.EndTime).Subscribe(_ => UpdateState());
        }

        private void UpdateState()
        {
            if (DateTime.Now < StartTime)
            {
                if (CurrentState != dontStarted)
                    CurrentState = dontStarted;
            }
            else if (DateTime.Now < EndTime)
            {
                if (CurrentState != started)
                    CurrentState = started;
            }
            else if (CurrentState != ended)
                CurrentState = ended;
            CurrentState.Update();
        }

        public Conference(string source) : this()
        {
            //EA 01.01.2020 10:23 - 02.02.2020 12:34 
            Match match = conferenceRegex.Match(source);
            Name = match.Groups["name"].Value;
            StartTime = new DateTime(
                Convert.ToInt32(match.Groups["startyear"].Value),
                Convert.ToInt32(match.Groups["startmounth"].Value),
                Convert.ToInt32(match.Groups["startday"].Value),
                Convert.ToInt32(match.Groups["starthour"].Value),
                Convert.ToInt32(match.Groups["startminute"].Value),
                0);
            EndTime = new DateTime(
                Convert.ToInt32(match.Groups["endyear"].Value),
                Convert.ToInt32(match.Groups["endmounth"].Value),
                Convert.ToInt32(match.Groups["endday"].Value),
                Convert.ToInt32(match.Groups["endhour"].Value),
                Convert.ToInt32(match.Groups["endminute"].Value),
                0);
        }



        public Conference(string name, DateTime start, DateTime end) : this()
        {
            Name = name;
            StartTime = start;
            EndTime = end;
        }

        private readonly DontStartedConferenceState dontStarted;
        private readonly StartedConferenceState started;
        private readonly EndedConferenceState ended;

        [Reactive] public ConferenceState CurrentState { get; set; }
        [Reactive] public string Name { get; set; }
        [Reactive] public DateTime StartTime { get; set; }
        [Reactive] public DateTime EndTime { get; set; }
        [Reactive] public TimeSpan RestTime { get; set; }

    }
}
