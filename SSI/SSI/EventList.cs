using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSI
{
    class EventList
    {
        public List<EventValues> values = new List<EventValues>();
    }
    class EventValues
    {
        public int number { set; get; }
        public string text { set; get; }
        public bool notif { set; get; }
    }

}
