using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiftChatMetro.FilterSystem
{
    public class FilterContext
    {
        private Tuple<string, int, Filter> filters;

        //static readonly FilterContext _instance = new FilterContext();
        //public static FilterContext Instance
        //{
        //    get
        //    {
        //        return _instance;
        //    }
        //}
        //FilterContext()
        //{
        //}

        private string name;
        private string type;
        private int id;
        private bool activated;

        public FilterContext(string name, string type)
        {
            this.name = name;
            this.type = type;
            this.id = id;
            this.activated = true;
        }

    }
}
