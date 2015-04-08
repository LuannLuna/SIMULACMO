using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Component
{
    class Berth
    {
        private double berth_speed;

        public void setBerthSpeed(double berth_speed)
        {
            this.berth_speed = berth_speed;
        }

        public double getBerthSpeed()
        {
            return berth_speed;
        }
    }
}
