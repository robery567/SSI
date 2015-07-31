using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SSI
{
    //clasa in a carei instanta este incarcat json-ul
    class EventList
    {
        public List<EventValues> values = new List<EventValues>();
    }
    //clasa ce contine elemente ale jsonului cu datele event-ului
    public class EventValues
    {
        public int number { set; get; }
        public string text { set; get; }
        public bool notif { set; get; }
        public string image64 { set; get; }
        public string title { set; get; }
        public string alarm { set; get; }
        public string notification { set; get; }
        public string objective { get; set; }
        public string color { get; set; }
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
