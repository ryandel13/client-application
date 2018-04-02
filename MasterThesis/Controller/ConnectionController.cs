using DotRas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterThesis.Controller
{
    class ConnectionController
    {
        public IEnumerable<RasEntry> GetAvailableEntries()
        {
            RasPhoneBook phoneBook = new RasPhoneBook();
            phoneBook = RasPhoneBook.Open(RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers));
            return phoneBook.Entries;
        }

        #region Properties
        public RasDialer Dialer { get; set; }
        public RasEntry Entry { get; set; }
        #endregion

        #region Methods
        public void ToggleConnection()
        {
            RasConnection conn = RasConnection.GetActiveConnections().Where(o => o.EntryName == "Internet").FirstOrDefault();
            if (conn == null)
            {
                RasDialer dialer = new RasDialer();
                dialer.PhoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers);
                foreach (RasEntry rE in GetAvailableEntries())
                {
                    if (rE.Name.Equals("Internet"))
                    {
                        dialer.EntryName = rE.Name;
                    }

                }
                dialer.DialAsync();
            }
            else
            {
                conn.HangUp();
            }
        }
        #endregion
    }
}
