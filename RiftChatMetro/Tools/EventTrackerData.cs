// http://json2csharp.com/
// http://www.newtonsoft.com/json/help/html/SerializingJSON.htm
// http://findnerd.com/list/view/How-to-parse-JSON-in-C/3881/

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiftChatMetro.Tools
{
    public class Datum
    {
        public string zone { get; set; }
        public int zoneId { get; set; }
        public string name { get; set; }
        public int? started { get; set; }
    }

    public class EventTrackerData
    {
        public string status { get; set; }
        public List<Datum> data { get; set; }
    }
}
