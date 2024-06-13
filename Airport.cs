using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laba10;

namespace DELETE
{
    public class Airport
    {
        public string Name { get; set; }
        public Stack<Aircraft> Aircrafts { get; set; }

        public Airport(string name)
        {
            Name = name;
            Aircrafts = new Stack<Aircraft>();
        }

        public void AddAircraft(Aircraft aircraft)
        {
            Aircrafts.Push(aircraft);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
