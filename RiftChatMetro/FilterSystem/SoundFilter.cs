﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RiftChatMetro.FilterSystem
{
    public class SoundFilter : Filter
    {
        private bool isActivated = true;
        private string name;

        public SoundFilter(string name)
        {
            this.name = name;
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

            if (split[0] == "ring")
            {
                line.Color = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(178, 34, 34));
                line.ContentColor = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(178, 34, 34));

                ring(
                    () => {System.Media.SystemSounds.Beep.Play(); },
                    () => { Console.WriteLine("beep"); },
                    Convert.ToInt32(split[1])
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
                        Thread.Sleep(1000);
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

        public string getIdentity()
        {
            return "soundfilter";
        }

    }
}
