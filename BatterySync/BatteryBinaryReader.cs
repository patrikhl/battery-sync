using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BatterySync
{
    class BatteryBinaryReader : BinaryReader
    {
        public BatteryBinaryReader(byte[] input) : base(new MemoryStream(input))
        {
        }
    }
}
