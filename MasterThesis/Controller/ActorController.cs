using MasterThesis.RESTInterface;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterThesis.Controller
{
    class ActorController
    {
        private static SerialPort sConn;

        public ActorController()
        {
            String portName = global::MasterThesis.Properties.Settings.Default.comPortActor;
            int baudRate;
            Int32.TryParse(global::MasterThesis.Properties.Settings.Default.baudRateActor.ToString(), out baudRate);

            sConn = new SerialPort(portName, baudRate);
        }

        public void HandleCommand(CommandMessage command)
        {
            try
            {
                sConn.Open();
                sConn.Write(command.GetCommand());
                sConn.Close();
            }
            catch (Exception e) { }
        }
    }
}
