using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterThesis.RESTInterface
{
    class CommandMessage
    {
        private string command;

        private string user;

        private string attributes;

        public CommandMessage(String message)
        {
            byte[] data = Convert.FromBase64String(message);
            string decodedString = Encoding.UTF8.GetString(data);

            string[] explosion = decodedString.Split('|');
            if (explosion.Length < 3)
            {
                throw new System.Exception();
            }
            command = explosion[0];
            user = explosion[1];
            attributes = explosion[2];
        }

        public string GetCommand()
        {
            return command;
        }
        public string GetUser()
        {
            return user;
        }
        public string GetAttributes()
        {
            return attributes;
        }
    }
}
