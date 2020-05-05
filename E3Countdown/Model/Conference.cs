﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace E3Countdown.Model
{
    public enum ConferenceState
    {
        DontStartedYet,
        Started,
        Ended
    }

    public class Conference : ReactiveObject
    {
        //EA 01.01.2020 10:23 - 02.02.2020 12:34 
        private static readonly Regex conferenceRegex = new Regex(@"(?<name>.+)\s+(?<startday>\d{2})\.(?<startmounth>\d{2})\.(?<startyear>\d{4})\s+(?<starthour>\d{2}):(?<startminute>\d{2})\s+\-\s+(?<endday>\d{2})\.(?<endmounth>\d{2})\.(?<endyear>\d{4})\s+(?<endhour>\d{2}):(?<endminute>\d{2})");

        private Conference()
        {
            
        }
        public Conference(string source) : this()
        {
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

        [Reactive] public string Name { get; set; }
        [Reactive] public DateTime StartTime { get; set; }
        [Reactive] public DateTime EndTime { get; set; }
        [Reactive] public TimeSpan RestTime { get; set; }

    }
}