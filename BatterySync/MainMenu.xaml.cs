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
        public MainMenu()
        {
            InitializeComponent();
            batteryList.ItemsSource = Switcher.pageSwitcher.batteries.Values;
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private void Battery_Click(object sender, RoutedEventArgs e)
        {
            Battery b = ((Button)sender).DataContext as Battery;
            Switcher.Switch(new BatteryMenu(ref b));
        }
    }
}
