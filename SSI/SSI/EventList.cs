using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

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
        public string image64 { set; get; }
    }
    class EventColor
    {
        public Color[] colors = new Color[] {
            Color.CadetBlue,
            Color.Brown,
            Color.LimeGreen,
            Color.Yellow,
            Color.SkyBlue,
            Color.Teal,
            Color.MidnightBlue,
            Color.Firebrick,
            Color.LightSeaGreen,
            Color.Chocolate
        };
        public EventColor()
        {

        }
    }
}
