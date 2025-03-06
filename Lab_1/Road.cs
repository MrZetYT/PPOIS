using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportNetwork
{
    public class Road : IRoads
    {
        private string name;
        private int distance;
        private string[] crossroads;
        private List<Transport> cars;
        private double congestion;

        public Road(string name, int distance, string[] crossroads, List<Transport> cars)
        {
            this.name = name;
            this.distance = distance;
            this.crossroads = crossroads;
            this.cars = cars;
            this.congestion = cars.Count * 0.7;
        }
        public string Name { get => name; }
        public int Distance { get => distance; }
        public string[] Crossroads { get => crossroads; }
        public List<Transport> Cars { get => cars; }
        public double Congestion { get => congestion; set => congestion = value*0.7; }
    }
}
