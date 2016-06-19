using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RiftChatMetro.FilterSystem
{
    public class AnalyticsNumberOfPlayersFilter : Filter
    {
        private bool isActivated = true;
        private string name;
        private int id;
        private bool isEU = true;

        private string path;
        private string currentFilePath;

        private List<string> players;
        private List<string> EUplayers;
        private List<string> NAplayers;
        private List<string> EUShards;
        private List<string> NAShards;

        public AnalyticsNumberOfPlayersFilter(string name, int id)
        {
            this.name = name;
            this.id = id;

            this.EUplayers = new List<string>();
            this.NAplayers = new List<string>();
            this.EUShards = new List<string>();
            this.NAShards = new List<string>();
            this.players = new List<string>();

            EUShards.Add("Brutwacht");
            EUShards.Add("Typhiria");
            EUShards.Add("Gelidra");
            EUShards.Add("Zaviel");
            EUShards.Add("Brisesol");
            EUShards.Add("Bloodiron");

            NAShards.Add("Hailol");
            NAShards.Add("Greybriar");
            NAShards.Add("Deepwood");
            NAShards.Add("Faeblight");
            NAShards.Add("Laethys");
            NAShards.Add("Seastone");
            NAShards.Add("Wolfsbane");

            this.path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\RIFT\\";
            if (File.Exists(path + "numberOfPlayersEU.txt"))
            {
                using (FileStream fs = new FileStream(path + "numberOfPlayersEU.txt", FileMode.Open, FileAccess.Read))
                using (StreamReader sw = new StreamReader(fs))
                {
                    string line = "";
                    while ((line = sw.ReadLine()) != null)
                    {
                        var test = line.Split(new char[] { '.' });
                        if (test.Count() < 2)
                        {
                            if (!EUplayers.Contains(line))
                                EUplayers.Add(line);
                        }
                            
                    }
                }
            }
            if (File.Exists(path + "numberOfPlayersNA.txt"))
            {
                using (FileStream fs = new FileStream(path + "numberOfPlayersNA.txt", FileMode.Open, FileAccess.Read))
                using (StreamReader sw = new StreamReader(fs))
                {
                    string line = "";
                    while ((line = sw.ReadLine()) != null)
                    {
                        var test = line.Split(new char[] { '.' });
                        if (test.Count() < 2)
                        {
                            if (!NAplayers.Contains(line))
                                NAplayers.Add(line);
                        }
                    }
                }
            }
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
            if (isActivated == false) return;
            if (line == null) return;
            if (line.Player == null) return;

            var test = line.Player.Split(new char[] { '.' });
            if (test.Count() >= 2)
                return;

            players = EUplayers;

            #region Choose EU/NA content
            if (!line.Player.Contains("@"))
            {
                if (isEU == true)
                {
                    this.currentFilePath = path + "numberOfPlayersEU.txt";
                    players = EUplayers;
                }
                else
                {
                    this.currentFilePath = path + "numberOfPlayersNA.txt";
                    players = NAplayers;
                }
            }
            else if (isEU == true && line.Player.Contains("@"))
            {
                foreach (string s in EUShards)
                {
                    if (line.Player.ToLower().Contains(s.ToLower()))
                    {
                        this.currentFilePath = path + "numberOfPlayersEU.txt";
                        isEU = true;
                        players = EUplayers;
                        break;
                    }
                    else
                    {
                        isEU = false;
                        this.currentFilePath = path + "numberOfPlayersNA.txt";
                        players = NAplayers;
                    }
                }
            }
            else if(isEU == false && line.Player.Contains("@"))
            {
                foreach (string s in NAShards)
                {
                    if (line.Player.ToLower().Contains(s.ToLower()))
                    {
                        this.currentFilePath = path + "numberOfPlayersNA.txt";
                        isEU = false;
                        players = NAplayers;
                        break;
                    }
                    else
                    {
                        this.currentFilePath = path + "numberOfPlayersNA.txt";
                        isEU = true;
                        players = EUplayers;
                    }
                }
            }
            #endregion

            if (!players.Contains(line.Player.ToLower()))
            {
                players.Add(line.Player.ToLower());
                using (FileStream fs = new FileStream(currentFilePath, FileMode.Append, FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(line.Player);
                }
            }
        }

        public object getObject()
        {
            return players.Count();
        }

        public long getIdentity()
        {
            return this.id;
        }

        public string getName()
        {
            return "numberofplayers" + Convert.ToString(getIdentity());
        }

        public void setColor(Color color)
        {
            return;
        }
    }
}