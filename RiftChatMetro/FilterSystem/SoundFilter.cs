using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RiftChatMetro.FilterSystem
{
    public class SoundFilter : Filter
    {
        private bool isActivated = true;
        private string name;
        private long id;

        public SoundFilter(string name, long id)
        {
            this.name = name;
            this.id = id;
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

            List<string> split = line.Content.Split(new char[] { ' ' }, 4).ToList<string>();
            if (split.Count < 3) return;
            if (split[2] != name) return;
            if (!line.Channel.ToLower().Trim().Equals("whisper"))
                return;

            if (split[0] == "ring")
            {
                line.Color = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(178, 34, 34));
                line.ContentColor = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(178, 34, 34));

                var numberOfRings = Convert.ToInt32(split[1]);
                if (numberOfRings >= 20) numberOfRings = 20;

                ring(
                    () => {System.Media.SystemSounds.Beep.Play(); },
                    () => { Console.WriteLine("beep"); },
                    numberOfRings
                    );
            }
        }

        static void ring(Action method, Action callback, int numberOfRings)
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                try
                {
                    for (int i = 0; i < numberOfRings; ++i)
                    {
                        Thread.Sleep(1250);
                        method();
                    }    
                }
                catch (ThreadAbortException) { /* dont report on this */ }
                catch (Exception ex)
                {
                }
                // note: this will not be called if the thread is aborted
                if (callback != null) callback();
            });
        }

        public long getIdentity()
        {
            return this.id;
        }

        public void setColor(Color color)
        {
            return;
        }

        public string getName()
        {
            return "soundfilter" + Convert.ToString(getIdentity());
        }

        public object getObject()
        {
            return null;
        }
    }
}
