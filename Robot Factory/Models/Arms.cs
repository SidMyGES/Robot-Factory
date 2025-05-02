using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robot_Factory.Models.Types;

namespace Robot_Factory.Models
{
    internal class Arms (ArmsType type) : Part("Arms")
    {
        public ArmsType Type { get; private set; } = type;
    }
}
