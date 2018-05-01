using MasterThesis.WindowComponents.Views;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
            while(true)
            {
                try
                {
                    String title = controller.GetTrack().TrackResource.Name;
                    {
                        if (title != null && !title.Equals(currentTitle))
                        {
                            String author = controller.GetTrack().ArtistResource.Name;
                            String album = controller.GetTrack().AlbumResource.Name;
                            Bitmap albumArt = controller.GetTrack().GetAlbumArt(SpotifyAPI.Local.Enums.AlbumArtSize.Size640);

                            SpotifyView sView = SpotifyView.getInstance();
                            double status = (controller.GetPosition() / controller.GetTrack().Length) * 100;

                            sView.UpdateSpotify(title, album, author, albumArt, status);
                        }
                    }
                } catch(Exception e)
                {
                    System.Console.WriteLine(e.StackTrace);
                }
                Thread.Sleep(500);
            }
        }

    }
}
