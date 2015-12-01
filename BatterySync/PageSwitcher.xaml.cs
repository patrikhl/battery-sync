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
using LibUsbDotNet;
using LibUsbDotNet.Main;
using LibUsbDotNet.DeviceNotify;

namespace BatterySync
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class PageSwitcher : Window
    {
        public Dictionary<string, Battery> batteries;
        
        public static UsbDeviceFinder usbFinder = new UsbDeviceFinder(0x1fc9, 0x0082);
        public static IDeviceNotifier usbDeviceNotifier = DeviceNotifier.OpenDeviceNotifier();

        public PageSwitcher()
        {
            InitializeComponent();
            // initialize battery dictionary list
            batteries = new Dictionary<string, Battery>();

            // set up the device notifier event
            usbDeviceNotifier.OnDeviceNotify += OnDeviceNotifyEvent;

            // add connected batteries
            UsbDevice usbDevice = UsbDevice.OpenUsbDevice(usbFinder);
            if (usbDevice != null)
            {
                batteries.Add(usbDevice.Info.SerialString.ToString(), new Battery(usbDevice));
            }

            // add batteries for testing
            //batteries.Add(new Battery { percentage = 20, chargeTime = 15, isFullyCharged = false, syncStatus = 0, health = 2, id = "1234ABCD" });
            //batteries.Add(new Battery { percentage = 100, chargeTime = 0, isFullyCharged = true, syncStatus = 2, health = 0, id = "8273LSKD" });
            //batteries.Add(new Battery { percentage = 73, chargeTime = 33, isFullyCharged = false, syncStatus = 1, health = 1, id = "9203USHF" });

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

        private void OnDeviceNotifyEvent(object sender, DeviceNotifyEventArgs e)
        {
            switch (e.EventType)
            {
                case EventType.DeviceArrival:
                    UsbDevice usbDevice = UsbDevice.OpenUsbDevice(usbFinder);
                    if (usbDevice != null)
                    {
                        batteries.Add(e.Device.SerialNumber, new Battery(usbDevice));
                        Switcher.Switch(new MainMenu());
                    }
                    break;
                case EventType.DeviceRemoveComplete:
                    batteries.Remove(e.Device.SerialNumber);
                    Switcher.Switch(new MainMenu());
                    break;
            }
        }
    }
}
