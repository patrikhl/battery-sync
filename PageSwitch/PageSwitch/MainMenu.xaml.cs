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


namespace PageSwitch
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : UserControl
    {
        public MainMenu()
        {
            InitializeComponent();
            List<Battery> batteries = new List<Battery>();
            batteries.Add(new Battery() { percentage = 20, syncStatus = 1 , chargeTime = 15});
            batteries.Add(new Battery() { percentage = 100, syncStatus = 1 , chargeTime = 0});
            batteryList.ItemsSource = batteries;


        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private void Battery1MenuBtn_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new BatteryMenu1());
        }

        private void Battery2MenuBtn_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new BatteryMenu2());
        }

        private void Battery_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new BatteryMenu1());
        }
    }
}
