using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportNetwork
{
    public class TrafficLight
    {
        public enum TrafficLightState { Red, Yellow, Green }
        public TrafficLightState State { get; set; }

        public TrafficLight()
        {
            State = TrafficLightState.Red;
        }

        public void ChangeState()
        {
            State = State switch
            {
                TrafficLightState.Red => TrafficLightState.Yellow,
                TrafficLightState.Yellow => TrafficLightState.Green,
                TrafficLightState.Green => TrafficLightState.Red,
                _ => TrafficLightState.Red
            };
        }
    }
}
