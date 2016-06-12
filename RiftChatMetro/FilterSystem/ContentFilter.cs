using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RiftChatMetro.FilterSystem
{
    public class ContentFilter : Filter
    {
        private bool isActivated;
        private string content;
        private Color color;
        private long identity;

        public ContentFilter(string content, long identity)
        {
            this.isActivated = true;
            this.content = content;
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
            if (isActivated == true && line.Content.ToLower().Contains(content.ToLower()))
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
            return "contentfilter" + Convert.ToString(identity);
        }
    }
}
