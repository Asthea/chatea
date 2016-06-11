using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RiftChatMetro.FilterSystem
{
    public interface Filter
    {
        void filter(Line line);
        void deactivate();
        void activate();
        string getIdentity();
        void setColor(Color color);
    }
}
