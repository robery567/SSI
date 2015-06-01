using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSI
{
    class DateConverter
    {
        public DateConverter()
        {
        }
       public int GetDateInt(string date)
       {
           switch(date)
           {
               case "January":
                   return 1;                   
               case "February":
                   return 2;
               case "March":
                   return 3;
               case "April":
                   return 4;
               case "May":
                   return 5;
               case "June":
                   return 6;
               case "July":
                   return 7;
               case "August":
                   return 8;
               case "September":
                   return 9;
               case "October":
                   return 10;
               case "November":
                   return 11;
               case "December":
                   return 12;
           }
           return 1;
       }
            public int GetDayInt(string dayWeek)
            {
                switch (dayWeek)
                {
                    case "Monday":
                        return 1;
                    case "Tuesday":
                        return 2;
                    case "Wednesday":
                        return 3;
                    case "Thursday":
                        return 4;
                    case "Friday":
                        return 5;
                    case "Saturday":
                        return 6;
                    case "Sunday":
                        return 0;
                }
                return 1;
            }
        
    }
}
