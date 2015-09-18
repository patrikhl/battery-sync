using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BatterySync
{

    public interface ISwitchable
    {
        void UtilizeState(object state);
    }
}
