using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Component
{
    class Ship
    {
        private int tide_arrival;
        private double[] cargo;

        public void setTideArrival(int tide_arrival){
            this.tide_arrival = tide_arrival;
        }

        public int getTideArrival(){
            return tide_arrival;
        }

        public void setCargo(double[] cargo) {
            this.cargo = cargo;
        }

        public double[] getCargo() {
            return cargo;
        }
    }
}
