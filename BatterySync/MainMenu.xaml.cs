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
using System.Windows.Ink;


namespace BatterySync
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : UserControl
    {
        List<Battery> batteries;

        public MainMenu()
        {
            InitializeComponent();
            // initialize battery list
            batteries = new List<Battery>();
            batteryList.ItemsSource = batteries;
            // add to batteries for testing
            batteries.Add(new Battery { percentage = 20, chargeTime = 15, isFullyCharged = false, syncStatus = 0 });
            batteries.Add(new Battery { percentage = 100, chargeTime = 0, isFullyCharged = true, syncStatus = 2 });
            batteries.Add(new Battery { percentage = 73, chargeTime = 33, isFullyCharged = false, syncStatus = 1 });
            //batteries.Add(new Battery(20, 15, false, 0));
            //batteries.Add(new Battery(100, 0, true, 1));
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private void Battery_Click(object sender, RoutedEventArgs e)
        {
            Battery b = ((Button)sender).DataContext as Battery;
            Switcher.Switch(new BatteryMenu(b));
        }
    }
}
