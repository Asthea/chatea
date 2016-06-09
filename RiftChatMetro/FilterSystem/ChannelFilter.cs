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
        private Dictionary<string, List<string>> channelNames;

        public ChannelFilter()
        {
            channelColors = new Dictionary<string, Brush>();
            colors = new Dictionary<string, Brush>();
            channelNames = new Dictionary<string, List<string>>();

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
            if (channelNames.ContainsKey(line.Channel.ToLower()))
            {
                line.Color = channelColors[line.Channel];
            }
            else
            {
                foreach (KeyValuePair<string, List<string>> kvp in channelNames)
                {
                    foreach (var element in kvp.Value)
                    {
                        string pattern = "\\b" + element.ToLower() + "\\b";
                        if (Regex.IsMatch(line.Channel.ToLower(), pattern, RegexOptions.IgnoreCase))
                        {
                            line.Color = channelColors[line.Channel];
                        }
                    }
                }
            }
        }

        public string getIdentity()
        {
            return "ChannelFilter";
        }

        private void initialize()
        {
            colors.Add("orange", new SolidColorBrush(Color.FromRgb(255, 165, 0)));
            colors.Add("blue", new SolidColorBrush(Color.FromRgb(20, 130, 180)));
            colors.Add("forestgreen", Brushes.ForestGreen);
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

            channelNames.Add("level 1-29", new List<string>() { "level 1-29", "stufe 1-29", "niveau 1-29" });
            channelNames.Add("level 30-49", new List<string>() { "level 30-49", "stufe 30-49", "niveau 30-49" });
            channelNames.Add("level 50-59", new List<string>() { "level 50-59", "stufe 50-59", "niveau 50-59" });
            channelNames.Add("level 60-64", new List<string>() { "level 60-64", "stufe 60-64", "niveau 60-64" });
            channelNames.Add("level 65", new List<string>() { "level 65", "stufe 65", "niveau 65" });
            channelNames.Add("guild", new List<string>() { "guild" , "gilde"});
            channelNames.Add("whisper", new List<string>() { "whisper", "flüstert", "to", "an" });
            channelNames.Add("crossevents", new List<string>() { "crossevents" });
            channelNames.Add("group", new List<string>() { "group", "gruppe" });
            channelNames.Add("raid", new List<string>() { "raid", "schlachtzug" });
        }
    }

}
