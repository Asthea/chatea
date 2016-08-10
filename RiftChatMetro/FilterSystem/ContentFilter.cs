using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RiftChatMetro.FilterSystem
{
    public class ContentFilter : Filter
    {
        private bool isActivated;
        private string content;
        private Color color;
        private long identity;
        //private bool useRegex;

        public ContentFilter(string content, long identity)
        {
            this.isActivated = true;
            this.content = content;
            this.identity = identity;

            //if (content.Contains("[") && content.Contains("]"))
            //{
            //    this.useRegex = true;
            //}
            //else
            //{
            //    this.useRegex = false;
            //}
        }

        public void activate()
        {
            this.isActivated = true;
        }

        public void deactivate()
        {
            this.isActivated = false;
        }

        public void filter(Line line)
        {

            #region Build Pattern
            var withBracketsMatches = Regex.Matches(content, @"\[.*?\]");
            var withoutBracketsMatches = Regex.Matches(content, @"[^\[xn+\]]+");

            List<string> withBracketsList = new List<string>();
            foreach (Match m in withBracketsMatches)
            {
                withBracketsList.Add(m.Value);
            }

            string[] splitContent = null;
            List<string> splitList = new List<string>();

            foreach (string s in content.Split('['))
            {
                splitContent = s.Split(']');
                splitList.AddRange(splitContent);
            }

            StringBuilder pattern = new StringBuilder();
            foreach (string s in splitList)
            { 
                if (withBracketsList.Count > 0)
                {
                    if (withBracketsList[0].Equals(@"["+s+@"]"))
                    {
                        if (withBracketsList[0].Equals(@"[n]"))
                            pattern.Append(@"\d");
                        if (withBracketsList[0].Equals(@"[nn]"))
                            pattern.Append(@"\d+");
                        if (withBracketsList[0].Equals(@"[x]"))
                            pattern.Append(@"\w");
                        if (withBracketsList[0].Equals(@"[xx]"))
                            pattern.Append(@"\w+");

                        withBracketsList.RemoveAt(0);
                        continue;
                    }
                }
                for (int i = 0; i < s.Length; ++i)
                {
                    pattern.Append(@"[");
                    pattern.Append(Char.ToLower(s[i]));
                    pattern.Append(Char.ToUpper(s[i]));
                    pattern.Append(@"]");
                }
            }
            #endregion

            Match match = Regex.Match(line.Content, pattern.ToString());

            if (isActivated == true && match.Success)
            {
                line.Color = new System.Windows.Media.SolidColorBrush(this.color);
                line.ContentColor = new System.Windows.Media.SolidColorBrush(this.color);
            }
        }

        public long getIdentity()
        {
            return this.identity;
        }

        public void setColor(Color color)
        {
            this.color = color;
        }

        public string getName()
        {
            return "contentfilter" + Convert.ToString(identity);
        }

        public object getObject()
        {
            return null;
        }
    }
}
