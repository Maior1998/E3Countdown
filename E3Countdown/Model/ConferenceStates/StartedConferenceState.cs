using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E3Countdown.Model.ConferenceStates
{
    public class StartedConferenceState : ConferenceState
    {
        public StartedConferenceState(Conference source) : base(source, "Идёт прямо сейчас")
        {
        }

        public override void Update()
        {
            attachedConference.RestTime = attachedConference.EndTime - DateTime.Now;
        }
    }
}
