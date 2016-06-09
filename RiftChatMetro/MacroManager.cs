using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RiftChatMetro
{
    public class MacroManager
    {

        public static void Initialize()
        {
            
            //colorCodingD.Add("global", new SolidColorBrush(Color.FromRgb(20, 130, 180)));
            //languageMappingD.Add(new List<string>() { 
            //    "level 1-29", "stufe 1-29", "niveau 1-29" },
            //    "global"
            //    );
            //languageMappingD.Add(new List<string>() { 
            //    "level 30-49", "stufe 30-49", "niveau 30-49" },
            //    "global"
            //    );
            //languageMappingD.Add(new List<string>() { 
            //    "level 50-59", "stufe 50-59", "niveau 50-59" },
            //    "global"
            //    );
            //languageMappingD.Add(new List<string>() { 
            //    "level 60-64", "stufe 60-64", "niveau 60-64" },
            //    "global"
            //    );
            //languageMappingD.Add(new List<string>() { 
            //    "level 65", "stufe 65", "niveau 65" },
            //    "global"
            //    );

            //colorCodingD.Add("guild", Brushes.ForestGreen);
            //languageMappingD.Add(new List<string>() { 
            //    "gilde", "guild"},
            //    "guild"
            //    );

            //colorCodingD.Add("whisper", new SolidColorBrush(Color.FromRgb(233, 24, 95)));
            //languageMappingD.Add(new List<string>() { 
            //    "whispers", "flüstert", "to", "an"},
            //    "whisper"
            //    );

            //colorCodingD.Add("crossevents", new SolidColorBrush(Color.FromRgb(255, 165, 0)));
            //languageMappingD.Add(new List<string>() {
            //    "crossevents", "CrossEvents"},
            //    "crossevents"
            //    );

            //colorCodingD.Add("group", new SolidColorBrush(Color.FromRgb(255, 127, 80)));
            //languageMappingD.Add(new List<string>() {
            //    "gruppe", "group"},
            //    "group"
            //    );

            //colorCodingD.Add("raid", new SolidColorBrush(Color.FromRgb(255, 99, 71)));
            //languageMappingD.Add(new List<string>() {
            //    "schlachtzug", "raid"},
            //    "raid"
            //    );

            //colorCodingD.Add("unknown", Brushes.Gray);
        }

        public static Brush getColorCode(string channel)
        {
            Brush b = colorCodingD[channel];
            return b != null ? b : new SolidColorBrush(Colors.Red);
        }

        public static string getMacroText(string text)
        {
            string translation = lookup(text);
            return (translation == null ? "unknown" : translation);
        }

        public static void addMapping(string text, Brush color)
        {
            if (text == null) throw new NullReferenceException();
            if (!colorCodingD.ContainsKey(text))
            {
                colorCodingD.Add(text, color);
                languageMappingD.Add(new List<string>() { text }, text);
            }
        }

        public static void deleteMapping(string text)
        {
            if (colorCodingD.ContainsKey(text))
            {
                colorCodingD.Remove(text);
                languageMappingD.Remove(new List<string>() { text });
            }
        }

        private static string lookup(string text)
        {
            foreach (KeyValuePair<List<string>, string> kvp in languageMappingD)
            {
                foreach (var element in kvp.Key)
                {
                    string pattern = "\\b" + element.ToLower() + "\\b";
                    if (Regex.IsMatch(text.ToLower(), pattern, RegexOptions.IgnoreCase))
                    {
                        return kvp.Value;
                    }
                }
            }

            return null;
        }

        private static Dictionary<List<string>, string> languageMappingD = new Dictionary<List<string>, string>();
        private static Dictionary<string, Brush> colorCodingD = new Dictionary<string, Brush>();
    }
}

