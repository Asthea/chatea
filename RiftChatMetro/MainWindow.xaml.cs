using MahApps.Metro.Controls;
using RiftChatMetro.FilterSystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RiftChatMetro
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        string fullPath;

        private Reader reader;
        private LineObjectPool lineOP = new LineObjectPool();
        private ContentControls cc;
        private StorageContainer sc;
        private Dictionary<string, Filter> filterD;
        private LineEvaluator lEval;

        private System.Windows.Threading.DispatcherTimer dispatcherTimer1 = new System.Windows.Threading.DispatcherTimer();
        private System.Windows.Threading.DispatcherTimer dispatcherTimer2 = new System.Windows.Threading.DispatcherTimer();

        private ObservableCollection<CheckedListItem> clItemL = new ObservableCollection<CheckedListItem>();
        private ObservableCollection<CheckedListItem> alertsCLItemL = new ObservableCollection<CheckedListItem>();

        public MainWindow()
        {
            InitializeComponent();
            //MacroManager.Initialize();

            filterD = new Dictionary<string, Filter>();
            sc = new StorageContainer("chat");
            cc = new ContentControls("chat", lineOP);
            cc.addDataGrid("global", dg1);
            cc.addDataGrid("whisper", dg2);
            cc.addDataGrid("guild", dg3);
            this.lEval = new LineEvaluator();

            lb1.ItemsSource = clItemL;
            alertsLB1.ItemsSource = alertsCLItemL;

            dispatcherTimer1.Tick += dispatcherTimer_Tick1;
            dispatcherTimer1.Interval = new TimeSpan(0, 0, 0, 0, 10);
            dispatcherTimer1.Start();
            //  --------------------------------------------------- //
            dispatcherTimer2.Tick += dispatcherTimer_Tick2;
            dispatcherTimer2.Interval = new TimeSpan(0, 0, 0, 0, 10);
            dispatcherTimer2.Start();

            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\RIFT\\";
            string logFilename = "log.txt";

            fullPath = path + logFilename;

            Filter channelFilter = new ChannelFilter();
            lEval.registerFilter(channelFilter);

            this.reader = new Reader(fullPath, sc, lEval);          
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void dispatcherTimer_Tick1(object sender, EventArgs e)
        {
            reader.read();
        }

        private void dispatcherTimer_Tick2(object sender, EventArgs e)
        {
            Line line = reader.getStorage().getNextElement();
            //if (line == null) return;
            cc.write(line);
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            reader.destroyReader();
        }

        private void b1_Click(object sender, RoutedEventArgs e)
        {
            if (tb1.Text == "" || tb1.Text == "Hier Suchmaske einfügen ...")
            {
                MessageBox.Show("Es muss erst eine Suchmaske angegeben werden!");
                return;
            }

            CheckedListItem cli = new CheckedListItem();
            cli.Content = tb1.Text;
            cli.IsChecked = true;

            //MacroManager.addMapping(cli.Content, new SolidColorBrush(colpi1.SelectedColor));
            //sc.registerCustomMask(cli.Content, new SolidColorBrush(colpi1.SelectedColor));

            Filter playerFilter = new LFPlayer(tb1.Text);
            playerFilter.setColor(colpi1.SelectedColor);
            this.filterD.Add(tb1.Text, playerFilter);
            lEval.registerFilter(playerFilter);
            //this.reader.getStorage().registerFilter(playerFilter);

            clItemL.Add(cli);
            lb1.Items.Refresh();
        }

        private void tb1_GotFocus(object sender, RoutedEventArgs e)
        {
            tb1.Text = "";
        }

        private void b2_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in clItemL)
            {
                //MacroManager.deleteMapping(item.Content);
                //sc.unregisterCustomMasks();
            }
            foreach(KeyValuePair<string, Filter> kvp in filterD)
            {
                //this.reader.getStorage().unregisterFilter(kvp.Value);
            }

            clItemL.Clear();
            lb1.Items.Refresh();
        }

        private System.Windows.Forms.ColorDialog cd = new System.Windows.Forms.ColorDialog();

        private void b_apiKey_Click(object sender, RoutedEventArgs e)
        {
            Translator t = new Translator(tb_apiKey.Text);
        }

        private void tb_apiKey_GotFocus(object sender, RoutedEventArgs e)
        {
            tb_apiKey.Text = "";
        }

        private void dg1_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dg1.SelectedItem == null) return;
            var selectedItem = dg1.SelectedItem as Line;
            //Debug.WriteLine(selectedItem.Content);

            //  TODO: Implement clipboard action!
        }

        private void dg1_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            cc.toggleAutoScroll(false);
        }

        private void dg1_MouseLeave(object sender, MouseEventArgs e)
        {
            cc.toggleAutoScroll(true);
        }

        private void dg1_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            try
            {
                Line test = ((Line)(e.Row.DataContext));
                e.Row.Background = test.Color;
                if (test.ContentColor != null)
                    e.Row.Background = test.ContentColor;
            }
            catch
            {
            }
        }

        private void soundfilterCB_Click(object sender, RoutedEventArgs e)
        {
            if (soundfilterTB.Text == "") return;

            soundfilterCB.Content = soundfilterTB.Text;

            if (soundfilterCB.IsChecked == true)
            {
                Filter filter = new SoundFilter(soundfilterTB.Text);
                this.filterD.Add((string)soundfilterCB.Content, filter);
                //this.reader.getStorage().registerFilter(filter);
            }
            else
            {
                //this.reader.getStorage().unregisterFilter(filterD[(string)soundfilterCB.Content]);
                this.filterD.Remove((string)soundfilterCB.Content);
            }
        }

        private void alertsB1_Click(object sender, RoutedEventArgs e)
        {
            if (alertsTB1.Text == "" || alertsTB1.Text == "Hier Suchmaske einfügen ...")
            {
                MessageBox.Show("Es muss erst eine Suchmaske angegeben werden!");
                return;
            }

            CheckedListItem cli = new CheckedListItem();
            cli.Content = alertsTB1.Text;
            cli.IsChecked = true;

            Filter filter = new LFPlayer(alertsTB1.Text);
            this.filterD.Add(alertsTB1.Text, filter);
            //this.reader.getStorage().registerFilter(filter);

            alertsCLItemL.Add(cli);
            alertsLB1.Items.Refresh();
        }

        private void alertsB2_Click(object sender, RoutedEventArgs e)
        {
            //foreach (var item in alertsCLItemL)
            //{
            //    MacroManager.deleteMapping(item.Content);
            //    sc.unregisterCustomMasks();
            //}

            foreach (KeyValuePair<string, Filter> kvp in filterD)
            {
                //this.reader.getStorage().unregisterFilter(kvp.Value);
            }

            alertsCLItemL.Clear();
            alertsLB1.Items.Refresh();
        }

        private void alertsTB1_GotFocus(object sender, RoutedEventArgs e)
        {
            alertsTB1.Text = "";
        }
    }
}

/**
 * RESOURCES
 * - Programmatically add row color: https://stackoverflow.com/questions/10056657/change-wpf-datagrid-row-color
 * 
 */
