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
        private long identity;

        public LFPlayer(string playerName, long identity)
        {
            this.isActivated = true;
            this.playerName = playerName;
            this.identity = identity;
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
            var player = "";
            if (line.Player.Contains("@"))
            {
                player = line.Player.Split(new char[] { '@' })[0].ToLower();
            }
            else
            {
                player = line.Player.ToLower();
            }

            if (isActivated == true && player.Contains(playerName.ToLower()))
            {
                line.Color = new System.Windows.Media.SolidColorBrush(this.color);
                line.ContentColor = new System.Windows.Media.SolidColorBrush(this.color);
            }
        }

        public long getIdentity()
        {
            return this.identity;
        }

        public void setColor(Color color)
        {
            this.color = color;
        }

        public string getName()
        {
            return "lfplayer" + Convert.ToString(identity);
        }

        public object getObject()
        {
            return null;
        }
    }
}
