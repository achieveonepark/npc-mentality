using System;

namespace NpcMentality
{
    [Serializable]
    public class TimeSchedule
    {
        public string Id;
        public int OpenHour;
        public int CloseHour;
        public Action OnOpen;
        public Action OnClose;
    }
}
