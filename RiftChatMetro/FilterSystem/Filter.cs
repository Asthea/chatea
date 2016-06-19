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
        long getIdentity();
        string getName();
        void setColor(Color color);
        object getObject();
        //void setID(int id);
    }
}
