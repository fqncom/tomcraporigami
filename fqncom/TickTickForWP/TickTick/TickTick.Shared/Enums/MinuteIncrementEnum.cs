using System;
using System.Collections.Generic;
using System.Text;

namespace TickTick.Enum
{
    public static class MinuteIncrementEnum
    {
        public const string No_Reminder = "";
        public const string At_StartTime = "TRIGGER:PT0S";
        //One_Minute = 1,
        public const string Five_Minute = "TRIGGER:-PT5M";//5,
        //Ten_Minutes = 10,
        //Fifteen_Minutes = 15,
        public const string Half_An_Hour = "TRIGGER:-PT30M";//30,
        public const string One_Hour = "TRIGGER:-PT1H";//60,
        //Four_Hours = 240,
        public const string One_Day = "TRIGGER:-PT24H";//1440
    }
    public static class MinuteDecreaseEnum
    {
        public const int Five_Minutes = 5;
        public const int Ten_Minutes = 10;
        //Half_An_Hour = 
        public const int One_Hour = 60;
        public const int Four_Hours = 240;
        public const int One_Day = 1440;
    }
}
