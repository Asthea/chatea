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
        private FilterContext context;

        public ContentFilter(string content)
        {
            this.isActivated = true;
            this.content = content;
            this.context = new FilterContext(content, getIdentity());
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
            if (line.Content.ToLower().Contains(content.ToLower()))
            {
                line.Color = new System.Windows.Media.SolidColorBrush(this.color);
                line.ContentColor = new System.Windows.Media.SolidColorBrush(this.color);
            }
        }

        public string getIdentity()
        {
            return "ContentFilter";
        }

        public void setColor(Color color)
        {
            this.color = color;
        }
    }
}
