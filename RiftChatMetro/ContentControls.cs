using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;

namespace RiftChatMetro
{
    public class ContentControls
    {
        public ContentControls(string identity, LineObjectPool lineOP)
        {
            this.identity = identity;
            this.dataGridD = new Dictionary<string, DataGrid>();
            this.dataGridL = new List<Line>();
            this.lineOP = lineOP;
        }

        public void add(string name, DataGrid dg)
        {
            if (name == null || dg == null) return;
            if (!dataGridD.ContainsKey(name))
            {
                this.dataGridD.Add(name, dg);
            }
        }

        public void addDataGrid(string name, DataGrid dg)
        {
            if (name == null || dg == null) return;
            this.dataGridD.Add(name, dg);
        }

        public void delete(string name)
        {
            if (name == null) return;
            if (dataGridD.ContainsKey(name))
            {
                this.dataGridD.Remove(name);
            }
        }

        public int getDataGridCount()
        {
            return this.dataGridD.Count;
        }

        public List<Line> getDataGrids()
        {
            return this.dataGridL;
        }

        public void write(Line line)
        {
            if (line == null || line.Channel == null) return;

            this.dataGridD["global"].Items.Add(line);
            if (this.scrollCondition)
            {
                var item = this.dataGridD["global"];
                item.ScrollIntoView(item.Items.GetItemAt(item.Items.Count - 1));
            }

            if (line.Channel != "global" && line.Channel != "unknown")
            {
                if (!this.dataGridD.ContainsKey(line.Channel))
                    return;

                this.dataGridD[line.Channel].Items.Add(line);
                if (this.scrollCondition)
                {
                    var item = this.dataGridD[line.Channel];
                    item.ScrollIntoView(item.Items.GetItemAt(item.Items.Count - 1));
                }
            }

            line.IsDisposable = true;
            
            //updateAll();
        }

        public void toggleAutoScroll(bool condition)
        {
            this.scrollCondition = condition;
        }

        public void setLock(bool state)
        {
            this.writerLockActive = state;   
        }

        private void updateAll()
        {
            if (this.scrollCondition == false) return;
            foreach (var item in dataGridD)
            {
                if (item.Value.Items.Count == 0) continue;
                item.Value.ScrollIntoView(item.Value.Items.GetItemAt(item.Value.Items.Count - 1));
            }
        }

        private string identity;
        private bool scrollCondition = true;
        private bool writerLockActive = false;

        private Dictionary<string, DataGrid> dataGridD;
        private List<Line> dataGridL;
        private LineObjectPool lineOP;

    }
}

/**
 * UNUSED CODE
 *
        var rtb = textboxD["global"];
        var tr = new TextRange(rtb.Document.ContentEnd, rtb.Document.ContentEnd);
        tr.Text = line.Text + "\r";
        tr.ApplyPropertyValue(TextElement.ForegroundProperty, line.Color);
 *
 */
