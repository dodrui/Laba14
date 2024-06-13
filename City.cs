using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DELETE
{
    public class City
    {
        public string Name { get; set; }
        public List<Airport> Airports { get; set; }

        public City(string name)
        {
            Name = name;
            Airports = new List<Airport>();
        }

        public void AddAirport(Airport airport)
        {
            Airports.Add(airport);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
