using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportNetwork
{
    public class Sidewalk
    {
        private string name;
        private int width;
        private string material;
        public string Name { get =>name; set=>name=value; }
        public int Width { get=>width; set=> width = value; }
        public string Material { get=>material; set=> material=value; }

        public Sidewalk(string name, int width, string material)
        {
            Name = name;
            Width = width;
            Material = material;
        }
    }
}
