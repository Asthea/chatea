using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiftChatMetro
{
    public class LineObjectPool
    {
        private Stack<Line> lineQ;

        public LineObjectPool()
        {
            this.lineQ = new Stack<Line>(10);
        }

        public void put(Line line)
        {
            lineQ.Push(line);
            Console.WriteLine("Stack size = " + lineQ.Count);
        }

        public Line get()
        {
            if (lineQ.Count == 0) return new Line();
            return lineQ.Pop();
        }

    }
}
