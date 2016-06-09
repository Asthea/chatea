using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiftChatMetro
{
    public class CheckedListItem : INotifyPropertyChanged
    {
        public CheckedListItem() { }

        public string Content
        {
            get { return this.content; }
            set
            {
                this.content = value;
                OnPropertyChanged("Content");
            }
        }
      
        public bool IsChecked
        {
            get { return this.isChecked; }
            set
            {
                this.isChecked = value;
                OnPropertyChanged("IsChecked");
            }
        }

        private string content;
        private bool isChecked;

        //  ------------------------------------------ //

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
