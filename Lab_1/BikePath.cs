using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportNetwork
{
    public class BikePath
    {
        private string name;
        private int length;
        private bool isDedicated;
        public string Name { get => name;}
        public int Length { get =>length;}
        public bool IsDedicated { get=>isDedicated;}

        public BikePath(string name, int length, bool isDedicated)
        {
            
            this.name = name;
            if(length>0)
                this.length = length;
            this.isDedicated = isDedicated;
        }
    }
}
