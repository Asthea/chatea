using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RiftChatMetro.FilterSystem
{
    public class ChannelFilter : Filter
    {
        private Dictionary<string, Brush> channelColors;
        private Dictionary<string, Brush> colors;
        private Dictionary<List<string>, string> channelNames;

        public ChannelFilter()
        {
            channelColors = new Dictionary<string, Brush>();
            colors = new Dictionary<string, Brush>();
            channelNames = new Dictionary<List<string>, string>();

            initialize();
        }

        public void activate()
        {
            throw new NotImplementedException();
        }

        public void deactivate()
        {
            throw new NotImplementedException();
        }

        public void filter(Line line)
        {
            if (line == null) return;
            string text = null;

            if (!line.Channel.Equals("whisper") || !line.Channel.Equals("unknown"))
            {
                List<string> split = line.Channel.Split(new char[] { ' ' }).ToList<string>();
                for (int i = 0; i < split.Count; ++i)
                {
                    text += split[i] + " ";
                }

                text = text.Trim();
            }
            else if (line.Channel == "whisper")
            {
                text = "whisper";
            }
            else
            {
                text = "unknown";
            }

            if (channelNames.ContainsValue(translateColor(text.ToLower())))
            {
                line.Color = channelColors[translateColor(text.ToLower())];
            }
            else
            {
                foreach (KeyValuePair<List<string>, string> kvp in channelNames)
                {
                    foreach (var element in kvp.Key)
                    {
                        string pattern = "\\b" + element.ToLower() + "\\b";
                        if (Regex.IsMatch(text.ToLower(), pattern, RegexOptions.IgnoreCase))
                        {
                            line.Color = channelColors[translateColor(text.ToLower())];
                        }
                        else
                        {
                            line.Color = colors["gray"];
                        }
                    }
                }
            }
        }

        public string getIdentity()
        {
            return "ChannelFilter";
        }

        private string translateColor(string channelName)
        {
            string color = "unknown";

            foreach (KeyValuePair<List<string>, string> kvp in channelNames)
            {
                foreach (string s in kvp.Key)
                {
                    if (s == channelName)
                        color = kvp.Value;
                }
            }

            return color;
        }

        private void initialize()
        {
            colors.Add("orange", new SolidColorBrush(Color.FromRgb(255, 165, 0)));
            colors.Add("blue", new SolidColorBrush(Color.FromRgb(20, 130, 180)));
            colors.Add("forestgreen", Brushes.ForestGreen);
            colors.Add("gray", Brushes.Gray);
            colors.Add("darkpink", new SolidColorBrush(Color.FromRgb(233, 24, 95)));
            colors.Add("coral", new SolidColorBrush(Color.FromRgb(255, 127, 80)));
            colors.Add("tomato", new SolidColorBrush(Color.FromRgb(255, 99, 71)));

            channelColors.Add("level 1-29", colors["blue"]);
            channelColors.Add("level 30-49", colors["blue"]);
            channelColors.Add("level 50-59", colors["blue"]);
            channelColors.Add("level 60-64", colors["blue"]);
            channelColors.Add("level 65", colors["blue"]);
            channelColors.Add("guild", colors["forestgreen"]);
            channelColors.Add("whisper", colors["darkpink"]);
            channelColors.Add("crossevents", colors["orange"]);
            channelColors.Add("group", colors["coral"]);
            channelColors.Add("raid", colors["tomato"]);

            channelNames.Add(new List<string>() { "level 1-29", "stufe 1-29", "niveau 1-29" }, "level 1-29");
            channelNames.Add(new List<string>() { "level 30-49", "stufe 30-49", "niveau 30-49" }, "level 30-49");
            channelNames.Add(new List<string>() { "level 50-59", "stufe 50-59", "niveau 50-59" }, "level 50-59");
            channelNames.Add(new List<string>() { "level 60-64", "stufe 60-64", "niveau 60-64" }, "level 60-64");
            channelNames.Add(new List<string>() { "level 65", "stufe 65", "niveau 65" }, "level 65");
            channelNames.Add(new List<string>() { "guild" , "gilde"}, "guild");
            channelNames.Add(new List<string>() { "whisper", "flüstert", "to", "an" }, "whisper");
            channelNames.Add(new List<string>() { "crossevents" }, "crossevents");
            channelNames.Add(new List<string>() { "group", "gruppe" }, "group");
            channelNames.Add(new List<string>() { "raid", "schlachtzug" }, "raid");
        }

        public void setColor(Color color)
        {
            throw new NotImplementedException();
        }

        long Filter.getIdentity()
        {
            // there is only one ChannelFilter
            return -1;
        }

        public string getName()
        {
            return "channelfilter";
        }
    }

}
