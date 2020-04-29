using MasterThesis.WindowComponents.Views;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MasterThesis.Controller
{
    class SpotifyUpdateThread
    {
        private static String currentTitle;

        private static SpotifyController controller = SpotifyController.GetInstance();

        public static void Start()
        {
            while (true)
            {
                if (controller.isInitialized()) { 
                try
                {

                    String title = controller.GetTrack().Item.Name;
                    {
                        if (title != null && !title.Equals(currentTitle))
                        {
                                String author = controller.GetTrack().Item.Artists[0].Name;
                                String album = controller.GetTrack().Item.Album.Name;
                                if (!File.Exists(@"c:\temp\spotify\" + album + ".png"))
                                {
                                    DownloadImage(controller.GetTrack().Item.Album.Images[0].Url, album);
                                }
                                FileStream file = new FileStream(@"c:\temp\spotify\" + album + ".png", FileMode.Open);
                                Bitmap albumArt = new Bitmap(file);

                            SpotifyView sView = SpotifyView.getInstance();
                                double status = controller.GetPosition();

                            sView.UpdateSpotify(title, author, album, albumArt, status);
                        }
                    }
                }
                catch (Exception e)
                {
                    System.Console.WriteLine(e.StackTrace);
                }
            }
                Thread.Sleep(500);
            }
        }

        private static void DownloadImage(String url, String albumName)
        {
            using (WebClient client = new WebClient())
            {
                //Bitte blockierend!
                client.DownloadFile(new Uri(url), @"c:\temp\spotify\" + albumName + ".png");
            }
        }

    }
}
