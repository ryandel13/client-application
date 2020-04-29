using DotRas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace MasterThesis.Controller
{
    class ConnectionController
    {
        private Boolean enabled = true;

        public IEnumerable<RasEntry> GetAvailableEntries()
        {
            RasPhoneBook phoneBook = new RasPhoneBook();
            //phoneBook = RasPhoneBook.Open(RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers));
            return phoneBook.Entries;
        }

        #region Properties
        public RasDialer Dialer { get; set; }
        public RasEntry Entry { get; set; }
        #endregion

        #region Methods
        public void ToggleConnection()
        {
            if (enabled)
            {
                ConnectionController.Disable("Ethernet2");
                enabled = false;
            }
            else
            {
                ConnectionController.Enable("Ethernet2");
                enabled = true;
            }
            //SelectQuery wmiQuery = new SelectQuery("SELECT * FROM Win32_NetworkAdapter WHERE NetConnectionId != NULL");
            //ManagementObjectSearcher searchProcedure = new ManagementObjectSearcher(wmiQuery);
            //foreach (ManagementObject item in searchProcedure.Get())
            //{
            //    if (((string)item["NetConnectionId"]) == "Ethernet 2")
            //    {
            //        item.InvokeMethod("Disable", null);
            //    }
            //}
            //RasConnection conn = RasConnection.GetActiveConnections().Where(o => o.EntryName == "Internet").FirstOrDefault();
            //if (conn == null)
            //{
            //    RasDialer dialer = new RasDialer();
            //    dialer.PhoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers);
            //    foreach (RasEntry rE in GetAvailableEntries())
            //    {
            //        if (rE.Name.Equals("Internet"))
            //        {
            //            dialer.EntryName = rE.Name;
            //        }

            //    }
            //    dialer.DialAsync();
            //}
            //else
            //{
            //    conn.HangUp();
            //}
        }
        #endregion

        static void Enable(string interfaceName)
        {
            System.Diagnostics.ProcessStartInfo psi =
                new System.Diagnostics.ProcessStartInfo("netsh", "interface set interface \"" + interfaceName + "\" enable");
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo = psi;
            p.Start();
        }

        static void Disable(string interfaceName)
        {
            System.Diagnostics.ProcessStartInfo psi =
                new System.Diagnostics.ProcessStartInfo("netsh", "interface set interface \"" + interfaceName + "\" disable");
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo = psi;
            p.Start();
        }
    }
}
