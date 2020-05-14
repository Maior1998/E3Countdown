using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E3Countdown.Model.ConferenceStates
{
    public class DontStartedConferenceState : ConferenceState
    {
        public DontStartedConferenceState(Conference source) : base(source, "Еще не началась")
        {
        }

        public override void Update()
        {
            attachedConference.RestTime = attachedConference.StartTime - DateTime.Now;
            if(attachedConference.RestTime.Hours==0 && 
               attachedConference.RestTime.Minutes==0 && 
               attachedConference.RestTime.Seconds==30)
                StaticFuncs.showBalloon($"Конференция {attachedConference.Name}",$"Начало через {StaticFuncs.GetTimeRest(attachedConference.RestTime)}");
        }
    }
}
