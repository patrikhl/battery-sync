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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class PageSwitcher : Window
    {
        public List<Battery> batteries;

        public PageSwitcher()
        {
            InitializeComponent();
            // initialize battery list
            batteries = new List<Battery>();
            // add batteries for testing
            batteries.Add(new Battery { percentage = 20, chargeTime = 15, isFullyCharged = false, syncStatus = 0, health = 2, id = "1234ABCD" });
            batteries.Add(new Battery { percentage = 100, chargeTime = 0, isFullyCharged = true, syncStatus = 2, health = 0, id = "8273LSKD" });
            batteries.Add(new Battery { percentage = 73, chargeTime = 33, isFullyCharged = false, syncStatus = 1, health = 1, id = "9203USHF" });
            // set pageSwitcher and switch to main menu
            Switcher.pageSwitcher = this;
            Switcher.Switch(new MainMenu());
        }

        public void Navigate(UserControl nextPage)
        {
            this.Content = nextPage;
        }

        public void Navigate(UserControl nextPage, object state)
        {
            this.Content = nextPage;
            ISwitchable s = nextPage as ISwitchable;

            if (s != null)
                s.UtilizeState(state);
            else
                throw new ArgumentException("NextPage is not ISwitchable! "
                  + nextPage.Name.ToString());
        }
    }
}
