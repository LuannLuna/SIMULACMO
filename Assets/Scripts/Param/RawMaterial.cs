using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Component
{
    public class RawMaterial
    {
        private string name;
        private double initial_stock;
        private double consumption;

        public void setName(string name) {
            this.name = name;
        }

        public string getName(){
            return name;
        }

        public void setInitialStock(double initial_stock)
        {
            this.initial_stock = initial_stock;
        }

        public double getInitialStock()
        {
            return initial_stock;
        }

        public void setConsumption(double consumption)
        {
            this.consumption = consumption;
        }

        public double getConsumption()
        {
            return consumption;
        }

    }
}
