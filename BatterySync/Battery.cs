using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatterySync
{
    public class Battery
    {
        public Battery(int p = 0, int ct = 0, bool ift = false, int ss = 0)
        {
            percentage = p;
            chargeTime = ct;
            isFullyCharged = ift;
            syncStatus = ss;
        }

        public int percentage { get; set; }
        public int chargeTime { get; set; }
        public bool isFullyCharged { get; set; }
        public int syncStatus { get; set; }
    }
}
