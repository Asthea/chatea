using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiftChatMetro.FilterSystem
{
    public interface Filter
    {
        void filter(Line line);
        void deactivate();
        void activate();
        string getIdentity();
    }
}
