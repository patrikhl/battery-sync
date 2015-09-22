using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BatterySync
{
    /// <summary>
    /// Interaction logic for BatteryMenu.xaml
    /// </summary>
    public partial class BatteryMenu : UserControl
    {
        Battery battery;

        public BatteryMenu(ref Battery b)
        {
            battery = b;
            this.DataContext = battery;
            InitializeComponent();
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new MainMenu());
        }

        private void Sync_Click(object sender, RoutedEventArgs e)
        {
            // set sync status to data syncronized
            battery.syncStatus = 0;
            // reload page
            Switcher.Switch(new BatteryMenu(ref battery));
        }
    }
}
