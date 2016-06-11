using RiftChatMetro.FilterSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * Reads a text file
 */
namespace RiftChatMetro
{
    class Reader
    {
        public Reader(string fullPath, StorageContainer sc, LineEvaluator lEval)
        {
            this.fullPath = fullPath;
            this.sc = sc;
            this.lEval = lEval;

            openFile();
        }

        public void read()
        {
            string readLine = file.ReadLine();
            if (readLine == "") return;
            if (readLine == null) return;

            if (currPosition <= fStream.Length)
            {
                currPosition = fStream.Position;
                Line line = lEval.createLine(readLine);
                sc.storeElement(line);
            }
        }

        public void destroyReader()
        {
            fStream.Close();
            file.Dispose();
            file.Close();

            // Clean file before exiting program
            //File.WriteAllText(fullPath, String.Empty);
        }

        public StorageContainer getStorage()
        {
            return sc;
        }

        private void openFile()
        {
            fStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            file = new System.IO.StreamReader(fStream);

            // Start reading at Begin/End of file
            this.fStream.Seek(0, SeekOrigin.Begin);
            currPosition = fStream.Position;
        }


        private string fullPath;
        private StorageContainer sc;

        private System.IO.StreamReader file;
        private FileStream fStream;
        private long currPosition;
        private List<Filter> filters;
        private LineEvaluator lEval;

    }
}

