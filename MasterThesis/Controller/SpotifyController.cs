using MasterThesis.Interfaces;
using SpotifyAPI.Local;
using SpotifyAPI.Local.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterThesis.Controller
{
    class SpotifyController : MediaPlayer
    {
        private static SpotifyController instance;

        private SpotifyLocalAPI _spotify;
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
            _spotify = new SpotifyLocalAPI(new SpotifyLocalAPIConfig
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
        }

        public Track GetTrack()
        {
            return _spotify.GetStatus().Track;
        }

        public double GetPosition()
        {
            return _spotify.GetStatus().PlayingPosition;
        }

        public void PlayPauseResume()
        {
            if (_spotify.GetStatus().Playing)
            {
                _spotify.Pause();
            }
            else
            {
                _spotify.Play();
            }
            
        }

        public void Stop()
        {
            _spotify.Pause();
        }

        public void Forward()
        {
            _spotify.Skip();
        }

        public void Rewind()
        {
            _spotify.Previous();
        }
    }
}
