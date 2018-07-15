using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterThesis.Controller
{

    static class Static
    {
        private static Process process;
        private static Process mediaProcess;

        public static void SetProcess(Process p)
        {
            process = p;
            process.Exited += process_Exited;
        }

        static void process_Exited(object sender, EventArgs e)
        {
            process = null;
        }

        public static void SetMediaProcess(Process p)
        {
            try
            {
                mediaProcess = p;
                mediaProcess.Exited += mediaProcess_Exited;
            }
            catch (Exception ex)
            {
            }
        }

        static void mediaProcess_Exited(object sender, EventArgs e)
        {
            mediaProcess = null;
        }

        public static Process GetProcess()
        {
            return process;
        }

        public static Process GetMediaProcess()
        {
            return mediaProcess;
        }

        public static void Shutdown()
        {
            if (process != null)
            {
                try
                {
                    mediaProcess.Close();
                    mediaProcess = null;
                }
                catch (Exception ex)
                {
                }
                try
                {
                    process.Close();
                    process = null;
                }
                catch (Exception ex)
                {
                }
            }
        }
    }
}
