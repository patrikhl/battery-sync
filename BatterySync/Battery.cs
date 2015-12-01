using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using LibUsbDotNet;
using LibUsbDotNet.Main;

namespace BatterySync
{
    public class Battery
    {
        public Battery(UsbDevice usb = null)
        {
            usbDevice = usb;

            // If this is a "whole" usb device (libusb-win32, linux libusb)
            // it will have an IUsbDevice interface. If not (WinUSB) the 
            // variable will be null indicating this is an interface of a 
            // device.
            IUsbDevice wholeUsbDevice = usbDevice as IUsbDevice;
            if (!ReferenceEquals(wholeUsbDevice, null))
            {
                // This is a "whole" USB device. Before it can be used, 
                // the desired configuration and interface must be selected.

                // Select config #1
                wholeUsbDevice.SetConfiguration(1);

                // Claim interface #0.
                wholeUsbDevice.ClaimInterface(0);
            }

            // open read endpoint 1
            UsbEndpointReader batInfoReader = usbDevice.OpenEndpointReader(ReadEndpointID.Ep01);

            ErrorCode ec = ErrorCode.None;
            byte[] batInfoReadBuffer = new byte[64];
            int bytesRead = 0;

            // If the device hasn't sent data in the last 100 milliseconds,
            // a timeout error (ec = IoTimedOut) will occur.
            ec = batInfoReader.Read(batInfoReadBuffer, 100, out bytesRead);
            if (bytesRead == 0) throw new Exception("No more bytes!");

            // create BinaryReader and set source to readBuffer
            BatteryBinaryReader batInfoBReader = new BatteryBinaryReader(batInfoReadBuffer);

            // set battery variables using binary reader
            fwVersion = batInfoBReader.ReadByte() + ((float) batInfoBReader.ReadByte()) / 10;
            id = new string(batInfoBReader.ReadChars(6));
            name = new string(batInfoBReader.ReadChars(20));
            currentTime = batInfoBReader.ReadInt32();
            lastSyncTime = batInfoBReader.ReadInt32();
            capacity = batInfoBReader.ReadUInt16();
            lastFullCap = batInfoBReader.ReadUInt16();
            factoryCap = batInfoBReader.ReadUInt16();
            percentage = 100 * capacity / lastFullCap;
            int eepromPages = batInfoBReader.ReadUInt16();

            // get unix time
            int time = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            // check if battery time is correct
            if (currentTime != time || currentTime != time - 1)
            {
                // open write endpoint 1
                UsbEndpointWriter writer = usbDevice.OpenEndpointWriter(WriteEndpointID.Ep01);

                // convert time to byte array
                byte[] timeArray = BitConverter.GetBytes(time);

                int bytesWritten = 0;

                // write time to endpoint 1
                ec = writer.Write(timeArray, 100, out bytesWritten);
            }

            UsbTransfer usbReadTransfer;
            UsbEndpointReader dataReader = usbDevice.OpenEndpointReader(ReadEndpointID.Ep02);
            int bytesToRead = 128 * eepromPages;
            byte[] dataReadBuffer = new byte[bytesToRead];

            // reset error code
            ec = ErrorCode.None;

            // read logged data using async bulk transfer
            ec = dataReader.SubmitAsyncTransfer(dataReadBuffer, 0, bytesToRead, 100, out usbReadTransfer);

            // dispose of the usb transfer
            usbReadTransfer.Dispose();

            // create a string array with the lines of text
            var dataReadString = System.Text.Encoding.Default.GetString(dataReadBuffer);

            // Set a variable to the My Documents path
            string docPath =
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // write the string array to a new file named "DAS_battery.txt".
            using (StreamWriter outputFile = new StreamWriter(docPath + @"\DAS_battery.txt"))
            {
                outputFile.Write(dataReadString);
            }
        }

        public UsbDevice usbDevice { get; set; }

        public int percentage { get; set; }
        public int chargeTime { get; set; }
        public bool isFullyCharged { get; set; }
        public int syncStatus { get; set; }
        public int health { get; set; }

        public float fwVersion { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public int currentTime { get; set; }
        public int lastSyncTime { get; set; }
        public int capacity { get; set; }
        public int lastFullCap { get; set; }
        public int factoryCap { get; set; }

    }
}
