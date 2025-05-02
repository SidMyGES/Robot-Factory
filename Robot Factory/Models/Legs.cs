using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robot_Factory.Models.Types;

namespace Robot_Factory.Models
{
    internal class Legs(LegsType type): Part("Legs")
    {
        public LegsType Type { get; private set; } = type;
    }
}
