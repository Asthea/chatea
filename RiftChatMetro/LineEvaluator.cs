using RiftChatMetro.FilterSystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RiftChatMetro
{
    public class LineEvaluator : ObservableCollection<Line>
    {

        private Dictionary<string, Brush> customMaskD = new Dictionary<string, Brush>();
        private List<Filter> filterL;

        public LineEvaluator()
        {
            this.filterL = new List<Filter>();
        }

        public Line createLine(string text)
        {
            return evaluate(text);
        }

        //public void registerCustomMask(string mask, Brush color)
        //{
        //    if (customMaskD.ContainsKey(mask)) return;
        //    customMaskD.Add(mask, color);
        //}

        //public void unregisterCustomMasks()
        //{
        //    customMaskD.Clear();
        //}

        public void registerFilter(Filter filter)
        {
            this.filterL.Add(filter);
        }

        public void unregisterFilter(Filter filter)
        {
            this.filterL.Remove(filter);
        }

        // TODO: should actually be implemented with a strategy design pattern
        private Line evaluate(string text)
        {
            if (text.Substring(text.Length - 1).Contains("-"))
                return new Line();

            Line line = new Line();

            line.IsDisposable = false;
            line.Text = text;

            //  Get header
            List<string> split = text.Split(new char[] { ' ' }, 2).ToList<string>();
            line.Header = split[0].Substring(0, split[0].Length-1);

            //  Split by ':'character
            List<string> divide = split[1].Split(new char[] { ':' }, 2).ToList<string>();
            
            //  Get the actual line (text message)
            if (divide.Count == 1) line.Content = divide[0].Substring(0, divide[0].Length - 1);
            if (divide.Count >= 2)
            {
                string splitten = string.Join("", divide);
                line.Content = splitten.Substring(divide[0].Length, splitten.Length - divide[0].Length);
            }
            line.Content = line.Content.TrimStart();

            //  Get Player and Channel information via RegEx
            // if count == 2 -> normal channel
            var v = Regex.Matches(divide[0], @"\[([^]]*)\]");
            if (v.Count > 0 && v[0].Value.Contains("@"))
            {
                var shardName = v[0].Value.Split(new char[] { '@' }).ToList<string>();
                line.Shard = shardName[1].Substring(0, shardName[1].Length - 1);
                line.Channel = shardName[0].Substring(1, shardName[0].Length - 1);
            }
            else
            {
                line.Shard = "unknown";
            }

            //  Check type of message
            if (v.Count == 1)
            {
                List<string> split2 = text.Split(new char[] { ':' }).ToList<string>();

                //Console.WriteLine("v.Count = 1; Line: " + text);
                string check = split2[3].ToLower();
                foreach (string s in split)
                {
                    if (s.Equals("to") || s.Equals("an") || s.Equals("whisper") || s.Equals("flüster"))
                    {
                        line.Channel = "whisper";
                    }
                    else
                    {
                        line.Channel = "unknown";
                    }
                }
                line.Player = v[0].Value.Substring(1, v[0].Length - 2) + "    ";
            }
            else if (v.Count == 2)
            {
                var channelName = v[0].Value.Substring(1, v[0].Value.Length-2).Split(new char[] { '.' });
                if (channelName.Count() > 1 && channelName[1].Contains("@"))
                    channelName[1] = channelName[1].Split(new char[] { '@' })[0];
                if (channelName.Count() > 1)
                {
                    line.Channel = channelName[1].Trim();
                }
                else
                {
                    line.Channel = v[0].Value.Substring(1, v[0].Value.Length-2).Trim();
                }
                line.Player = v[1].Value.Substring(1, v[1].Length - 2) + "    ";
            }
            else
            {
                return null;
            }

            //  Get potential link candidates
            var links = Regex.Matches(divide[divide.Count - 1], @"\[([^]]*)\]");
            //  Add true link-candidates to line
            foreach (var item in links)
            {
                if (!item.ToString().Contains(line.Player) || !item.ToString().Contains( v[0].ToString() ))
                {
                    line.setLinks(item.ToString());
                }
            }

            //  Get color coding
            line.Color = null;
            line.ContentColor = null;

            //  Check for custom masks
            //evaluateCustomMasks(line);
            evaluateFilters(line);

            return line;
        }

        //private void evaluateCustomMasks(Line line)
        //{
        //    foreach (KeyValuePair<string, Brush> kvp in customMaskD)
        //    {
        //        if (line.Content.ToLower().Contains(kvp.Key.ToLower()))
        //        {
        //            line.ContentColor = kvp.Value;
        //        }
        //    }
        //}

        private void evaluateFilters(Line line)
        {
            foreach (Filter f in filterL)
            {
                f.filter(line);
            }
        }

    }
}
