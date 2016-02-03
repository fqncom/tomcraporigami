using System;
using System.Collections.Generic;
using System.Text;

namespace TickTick.Entity
{
    public class Loc
    {
        private double _longitude;

        public double Longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }
        private double _latitude;

        public double Latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }
    }
}
