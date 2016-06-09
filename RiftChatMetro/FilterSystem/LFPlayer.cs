using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiftChatMetro.FilterSystem
{
    public class LFPlayer : Filter
    {
        private bool isActivated;
        private string playerName;

        public LFPlayer(string playerName)
        {
            this.isActivated = true;
            this.playerName = playerName;
        }

        public void activate()
        {
            this.isActivated = true;
        }

        public void deactivate()
        {
            this.isActivated = false;
        }

        public void filter(Line line)
        {
            if (line.Player.ToLower() == playerName.ToLower())
            {
                line.Color = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(178, 34, 34));
                line.ContentColor = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(178, 34, 34));
            }
        }

        public string getIdentity()
        {
            return "LFPlayer";
        }
    }
}
