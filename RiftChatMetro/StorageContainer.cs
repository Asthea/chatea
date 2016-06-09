﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using RiftChatMetro.FilterSystem;

namespace RiftChatMetro
{
    class StorageContainer
    {

        public StorageContainer(string identity)
        {
            this.identity = identity;
            this.linesS = new Stack<Line>();
            //this.filterL = new List<Filter>();
            this.lEval = new LineEvaluator();
        }

        public void storeElement(string line)
        {
            if (line == null || line == "") return;
            Line l = lEval.createLine(line);
            linesS.Push(l);
        }

        public Line getNextElement()
        {
            if (linesS.Count == 0) return null;
            return linesS.Pop();
        }

        //public void registerCustomMask(string mask, Brush color)
        //{
        //    this.lEval.registerCustomMask(mask, color);
        //}

        //public void unregisterCustomMasks()
        //{
        //    this.lEval.unregisterCustomMasks();
        //}

        //public void registerFilter(Filter filter)
        //{
        //    filterL.Add(filter);
        //    this.lEval.registerFilter(filter);
        //}

        //public void unregisterFilter(Filter filter)
        //{
        //    this.lEval.unregisterFilter(filter);
        //    filterL.Remove(filter);
        //}

        public int getStorageSize()
        {
            return linesS.Count();
        }

        private string identity;
        private Stack<Line> linesS;
        private LineEvaluator lEval;
        //private List<Filter> filterL;

    }
}

