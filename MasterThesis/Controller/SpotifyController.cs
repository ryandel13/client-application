using MasterThesis.ExchangeObjects;
using MasterThesis.Interfaces;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Models;
/*using SpotifyAPI.Local;
using SpotifyAPI.Local.Models;*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Unosquare.Swan.Abstractions;

namespace MasterThesis.Controller
{
    class SpotifyController : MediaPlayer
    {
        private static SpotifyController instance;

        private static String _clientId = "ae99be430af246f8ba841e6fdbf3c2d3";

        private static String deviceId = "";
        internal bool isInitialized()
        {
            return this.initialized;
        }

        private SpotifyWebAPI spotifyWebAPI;

        private Boolean initialized = false;

        /*private SpotifyLocalAPI _spotify;*/
        public static SpotifyController GetInstance()
        {
            if(instance == null)
            {
                instance = new SpotifyController();
            }
            return instance;
        }
        private SpotifyController()
        {
            if (tokenIsExpired())
            {
                ImplicitGrantAuth auth = new ImplicitGrantAuth(_clientId, "http://localhost:8888", "http://localhost:8888", SpotifyAPI.Web.Enums.Scope.AppRemoteControl);

                auth.AuthReceived += async (sender, payload) =>
                {
                    auth.Stop(); // `sender` is also the auth instance
                    spotifyWebAPI = new SpotifyWebAPI()
                    {
                        TokenType = payload.TokenType,
                        AccessToken = payload.AccessToken


                    };
                    global::MasterThesis.Properties.Settings.Default.spotify_token = payload.AccessToken;
                    global::MasterThesis.Properties.Settings.Default.spotify_type = payload.TokenType;

                    DateTime dt = DateTime.Now.AddSeconds(payload.ExpiresIn);

                    global::MasterThesis.Properties.Settings.Default.spotify_tokenDate = dt.Ticks;

                    global::MasterThesis.Properties.Settings.Default.Save();

                    initialized = true;
                    if (global::MasterThesis.Properties.Settings.Default.spotify_deviceId != "")
                    {
                        spotifyWebAPI.TransferPlayback(global::MasterThesis.Properties.Settings.Default.spotify_deviceId, true);
                    }
                    // Do requests with API client
                };
                auth.Start(); // Starts an internal HTTP Server
                auth.OpenBrowser();
            } else
            {
                spotifyWebAPI = new SpotifyWebAPI()
                {
                    TokenType = global::MasterThesis.Properties.Settings.Default.spotify_type,
                    AccessToken = global::MasterThesis.Properties.Settings.Default.spotify_token
                };
                initialized = true;
                if (global::MasterThesis.Properties.Settings.Default.spotify_deviceId != "")
                {
                    spotifyWebAPI.TransferPlayback(global::MasterThesis.Properties.Settings.Default.spotify_deviceId, true);
                }
            }
          
            /*_spotify = new SpotifyLocalAPI(new SpotifyLocalAPIConfig
            {
                Port = 4381,
                HostUrl = "http://127.0.0.1"
            });
            if (!SpotifyLocalAPI.IsSpotifyRunning())
                return; //Make sure the spotify client is running
            if (!SpotifyLocalAPI.IsSpotifyWebHelperRunning())
                return; //Make sure the WebHelper is running

            if (!_spotify.Connect())
                return; //We need to call Connect before fetching infos, this will handle Auth stuff
                */
        }

        public static void SetDevice(string deviceId)
        {
            global::MasterThesis.Properties.Settings.Default.spotify_deviceId = deviceId;
            global::MasterThesis.Properties.Settings.Default.Save();
        }

        public PlaybackContext GetTrack()
        {
            return spotifyWebAPI.GetPlayingTrack();
        }
       
        public double GetPosition()
        {
            return (spotifyWebAPI.GetPlayingTrack().Item.DurationMs / spotifyWebAPI.GetPlayingTrack().ProgressMs) * 100;
        }

        public void PlayPauseResume()
        {
            if (spotifyWebAPI.GetPlayingTrack().IsPlaying)
            {
                spotifyWebAPI.PausePlayback();
            } else
            {
                //spotifyWebAPI.ResumePlayback(global::MasterThesis.Properties.Settings.Default.spotify_deviceId);
                spotifyWebAPI.TransferPlayback(global::MasterThesis.Properties.Settings.Default.spotify_deviceId, true);
            }
            
        }

        public void Stop()
        {
        }

        public void Forward()
        {
            spotifyWebAPI.SkipPlaybackToNext();
        }

        public void Rewind()
        {
            spotifyWebAPI.SkipPlaybackToPrevious();
        }

        public Boolean tokenIsExpired()
        {
            DateTime dt = new DateTime(global::MasterThesis.Properties.Settings.Default.spotify_tokenDate);
            if (DateTime.Now > dt)
            {
                return true;
            }
            else return false;
        }

        public List<SpotifyDevice> GetDevices()
        {
            List<SpotifyDevice> devices = new List<SpotifyDevice>();
            foreach(Device d in spotifyWebAPI.GetDevices().Devices) {
                SpotifyDevice sd = new SpotifyDevice(d.Name, d.Id);
                devices.Add(sd);
            }
            return devices;
        }
    }
}
