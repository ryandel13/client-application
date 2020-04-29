using MasterThesis.Controller;
using MasterThesis.ExchangeObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace MasterThesis.WindowComponents.Views
{
    /// <summary>
    /// Interaktionslogik für SpotifyView.xaml
    /// </summary>
    public partial class SpotifyView : UserControl
    {
        private static SpotifyView instance;

        private String currentTitle;

        private System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        

        public static SpotifyView getInstance()
        {
            if (instance == null)
            {
                instance = new SpotifyView();
            }
            return instance;
        }

        public SpotifyView()
        {
            InitializeComponent();



            //dispatcherTimer.Tick += new EventHandler(UpdateSpotify);
            //dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            //dispatcherTimer.Start();

            Thread th = new Thread(SpotifyUpdateThread.Start);
            th.IsBackground = true;
            th.Start();

            Title.Content = "";
            Author.Content = "";
            Album.Content = "";

            MediaPlayerController.SetCurrentPlayer(SpotifyController.GetInstance());
        }

        public void UpdateSpotify(String title, String album, String author, Bitmap albumArt, double status)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                try
                {
                    Title.Content = title;

                if (currentTitle == null || !currentTitle.Equals(Title.Content.ToString()))
                {
                    currentTitle = title; Title.Content.ToString();
                    Author.Content = author;
                    Album.Content = album;
                        if (albumArt != null)
                        {
                            AlbumArt.Source = BitmapHelper.getBitmapSourceFromBitmap(albumArt);
                        }
                }
                
                    Progress.Value = Convert.ToInt32(status);
                } catch(Exception e)
                {
                    System.Console.WriteLine(e.StackTrace);
                }
            }));
        }
    }
}
