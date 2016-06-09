using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RiftChatMetro
{
    public class Line
    {
        public string Channel { get; set; }
        public Brush Color { get; set; }
        public Brush ContentColor { get; set; }
        public string Text { get; set; }
        public string Content { get; set; }
        public string Header { get; set; }
        public string Player { get; set; }
        public bool IsDisposable { get; set; }

        public List<string> getLinks()
        {
            return this.links;
        }
        public void setLinks(string text)
        {
            this.links.Add(text);
        }

        private List<string> links = new List<string>();
    }
}
