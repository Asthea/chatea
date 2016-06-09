using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RiftChatMetro
{
    public class Translator
    {
        public Translator(string apiKey)
        {
            Translator.apiKey = apiKey;
        }

        public static string Translate(string word, string language)
        {
            string strUrl = "https://translate.yandex.net/api/v1.5/tr.json/translate?&#8221";
            strUrl += "key=" + Translator.apiKey;
            strUrl += "&lang=en-" + language;
            strUrl += "&text=" + word;

            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;

            return wc.DownloadString(strUrl);;
        }

        private static string apiKey;
    }
}
