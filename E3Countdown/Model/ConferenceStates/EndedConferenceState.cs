using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E3Countdown.Model.ConferenceStates
{
    public class EndedConferenceState : ConferenceState
    {
        public EndedConferenceState(Conference source) : base(source, "Закончилась")
        {
        }

        public override void Update()
        {
            attachedConference.RestTime = DateTime.Now - attachedConference.EndTime;
        }
    }
}
