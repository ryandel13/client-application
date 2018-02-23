using eureka_sharpener;
using eureka_sharpener.elements;
using MasterThesis.ExchangeObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
    /// Interaktionslogik für MusicView.xaml
    /// </summary>
    public partial class MusicView : UserControl
    {
        private static MusicView instance;

        public static MusicView getInstance()
        {
            if (instance == null)
            {
                instance = new MusicView();
            }
            return instance;
        }

        private MusicView()
        {
            InitializeComponent();

            ThreadStart tStart = new ThreadStart(ReadMusicList);
            Thread t = new Thread(tStart);
            t.Start();
        }

        public static void ReadMusicList()
        {
            Eureka registry = new Eureka("localhost", 8761);
            Instance mssInstance = registry.ReadRegistry().FindInstance("music-streaming-service");

            retrieveMusicList(mssInstance);
        }

        private static void retrieveMusicList(Instance instance)
        {
            List<MusicTitleInfo> dataResponse = null;
            WebRequest request = WebRequest.Create(instance.homePageUrl + "music/");
            try
            {
                String remoteUrl = global::MasterThesis.Properties.Settings.Default.remoteConnection;
                //WebRequest request = WebRequest.Create(instance.homePageUrl + "music");
                WebResponse response = request.GetResponse();

                Stream dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();

                dataResponse = JsonConvert.DeserializeObject<List<MusicTitleInfo>>(responseFromServer);
            }
            catch (Exception e)
            {
                System.Console.Out.WriteLine("Error occured during webrequest to " + request.ToString());
            }
            if (dataResponse != null)
            {
                foreach (MusicTitleInfo p in dataResponse)
                {
                    MusicView.getInstance().AddMusicTitle(p);
                }
            }
        }

        public void AddMusicTitle(MusicTitleInfo p)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                MusicListControl mlc = new MusicListControl();
                mlc.Author.Content = p.author;
                mlc.Title.Content = p.title;

                TimeSpan ts = TimeSpan.FromMilliseconds(p.durationInMillis);
                
                mlc.Duration.Content = new DateTime(ts.Ticks).ToString("mm:ss");

                ContentDock.Children.Add(mlc);
            }));
        }
    }
}
