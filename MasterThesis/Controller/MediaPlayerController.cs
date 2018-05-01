using MasterThesis.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterThesis.Controller
{
    class MediaPlayerController
    {
        private static MediaPlayer currentPlayer;

        public static void SetCurrentPlayer(MediaPlayer player)
        {
            currentPlayer = player;
        }

        public static void PlayResume()
        {
            currentPlayer.PlayPauseResume();
        }

        public static void Forward()
        {
            currentPlayer.Forward();
        }
    }
}
