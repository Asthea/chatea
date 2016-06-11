using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RiftChatMetro.FilterSystem
{
    public class LFPlayer : Filter
    {
        private bool isActivated;
        private string playerName;
        private Color color;

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
            if (line.Player.ToLower().Contains(playerName.ToLower()))
            {
                line.Color = new System.Windows.Media.SolidColorBrush(this.color);
                line.ContentColor = new System.Windows.Media.SolidColorBrush(this.color);
            }
        }

        public string getIdentity()
        {
            return "LFPlayer";
        }

        public void setColor(Color color)
        {
            this.color = color;
        }
    }
}
