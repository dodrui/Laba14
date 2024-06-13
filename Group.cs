using Laba10;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DELETE
{
    public class Group : Aircraft 
    {
        public string Name { get; set; }

        public Group(string model, int productionYear, string engineType, int crewNumber, int number, string name)
            : base(model, productionYear, engineType, crewNumber, number)
        {
            Name = name;
        }

        public override string ToString()
        {
            return base.ToString() + $", модель = {Model}";
        }
    }
}
