/**
 * 
    https://stackoverflow.com/questions/4039376/how-to-get-selected-items-in-wpf-checkbox-listbox?rq=1
    https://stackoverflow.com/questions/21193242/wpf-checkedlistbox-how-to-get-selected-item
    https://stackoverflow.com/questions/6915329/listbox-and-finding-the-selected-checkbox?rq=1
    https://stackoverflow.com/questions/31015851/retrieving-selected-items-from-checked-list-box
    https://stackoverflow.com/questions/32905302/get-selected-items-from-a-list-box-with-check-boxes?rq=1
 *
 */

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
