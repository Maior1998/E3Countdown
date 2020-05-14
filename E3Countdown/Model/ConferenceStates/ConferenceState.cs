using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E3Countdown.Model.ConferenceStates
{
    public abstract class ConferenceState
    {
        public Conference attachedConference;
        public string StateName;
        protected ConferenceState(Conference source, string name)
        {
            attachedConference = source;
            StateName = name;
        }

        public abstract void Update();

        public override string ToString()
        {
            return StateName;
        }
    }
}
