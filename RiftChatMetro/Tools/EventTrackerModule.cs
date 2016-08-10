using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Dynamic;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Diagnostics;
using System.Data;

namespace RiftChatMetro.Tools
{
    public class EventTrackerModule
    {
        private Dictionary<string, EventTrackerData> eventTrackerDatas;
        private List<string> ids;

        public EventTrackerModule()
        {
            this.ids = new List<string>();
            this.eventTrackerDatas = new Dictionary<string, EventTrackerData>();
            this.ids.Add("1701");
            this.ids.Add("1702");
            this.ids.Add("1704");
            this.ids.Add("1706");
            this.ids.Add("1707");
            this.ids.Add("1708");
            this.ids.Add("1721");
            this.ids.Add("2702");
            this.ids.Add("2711");
            this.ids.Add("2714");
            this.ids.Add("2721");
            this.ids.Add("2722");
            this.ids.Add("2741");
        }

        public async Task<Dictionary<string, EventTrackerData>> Connect()
        {
            foreach (string id in ids)
            {
                try
                {
                    GetEventData(id).Wait();
                }
                catch (AggregateException ae)
                {
                    Debug.WriteLine(ae);
                }
            }

            return this.eventTrackerDatas;
        }

        public async Task GetEventData(string id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://web-api-us.riftgame.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync(string.Format("chatservice/zoneevent/list?shardId={0}", id)).Result;
                if (response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().Result;
                    var etd = JsonConvert.DeserializeObject<EventTrackerData>(json);
                    eventTrackerDatas.Add(id, etd);
                }
            }
        }
    }
}
