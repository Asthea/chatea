﻿using MahApps.Metro.Controls;
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
        private long id;

        private System.Windows.Threading.DispatcherTimer dispatcherTimer1 = new System.Windows.Threading.DispatcherTimer();
        private System.Windows.Threading.DispatcherTimer dispatcherTimer2 = new System.Windows.Threading.DispatcherTimer();
        private System.Windows.Threading.DispatcherTimer dispatcherTimer3 = new System.Windows.Threading.DispatcherTimer();

        private System.Windows.Forms.ColorDialog cd = new System.Windows.Forms.ColorDialog();

        public ObservableCollection<CheckedListItem> clItemL = new ObservableCollection<CheckedListItem>();
        private ObservableCollection<CheckedListItem> alertsCLItemL = new ObservableCollection<CheckedListItem>();

        public MainWindow()
        {
            InitializeComponent();

            filterD = new Dictionary<string, Filter>();
            sc = new StorageContainer("chat");
            cc = new ContentControls("chat", lineOP);
            cc.addDataGrid("global", dg1);
            //cc.addDataGrid("whisper", dg2);
            //cc.addDataGrid("guild", dg3);
            this.lEval = new LineEvaluator();
            this.id = 0;

            lb1.ItemsSource = clItemL;
            alertsLB1.ItemsSource = alertsCLItemL;

            //  --------------------------------------------------- //
            dispatcherTimer1.Tick += dispatcherTimer_Tick1;
            dispatcherTimer1.Interval = new TimeSpan(0, 0, 0, 0, 10);
            dispatcherTimer1.Start();
            //  --------------------------------------------------- //
            dispatcherTimer2.Tick += dispatcherTimer_Tick2;
            dispatcherTimer2.Interval = new TimeSpan(0, 0, 0, 0, 10);
            dispatcherTimer2.Start();
            //  --------------------------------------------------- //
            dispatcherTimer3.Tick += dispatcherTimer_Tick3;
            dispatcherTimer3.Interval = new TimeSpan(0, 0, 0, 0, 500);
            dispatcherTimer3.Start();

            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\RIFT\\";
            string logFilename = "log.txt";

            fullPath = path + logFilename;

            Filter channelFilter = new ChannelFilter();
            lEval.registerFilter(channelFilter);

            this.reader = new Reader(fullPath, sc, lEval);          
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Implement openfiledialog ...
        }

        private void dispatcherTimer_Tick1(object sender, EventArgs e)
        {
            reader.read();
        }

        private void dispatcherTimer_Tick2(object sender, EventArgs e)
        {
            Line line = reader.getStorage().getNextElement();
            cc.write(line);
        }

        private void dispatcherTimer_Tick3(object sender, EventArgs e)
        {
            List<CheckedListItem> isNotCheckedItem = clItemL.Where(item => item.IsChecked == false).ToList();
            isNotCheckedItem.AddRange(alertsCLItemL.Where(item => item.IsChecked == false).ToList());

            List<CheckedListItem> isCheckedItem = clItemL.Where(item => item.IsChecked == true).ToList();
            isCheckedItem.AddRange(alertsCLItemL.Where(item => item.IsChecked == true).ToList());
            
            // deactive unchecked filters
            foreach (CheckedListItem c in isNotCheckedItem)
            {
                var filters = lEval.getActivatedFiltersByID(c.ID);
                foreach (Filter f in filters)
                {
                    lEval.deactivateFilter(f);
                }
            }
            // activate checked filters
            foreach (CheckedListItem c in isCheckedItem)
            {
                var filters = lEval.getDeactivatedFiltersByID(c.ID);
                foreach (Filter f in filters)
                {
                    lEval.activateFilter(f);
                }
            }
        }

        private long generateID()
        {
            id += 1;
            return id;
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
            cli.ID = generateID();

            Filter playerFilter = new LFPlayer(tb1.Text, cli.ID);
            playerFilter.setColor(colpi1.SelectedColor);
            lEval.registerFilter(playerFilter);

            Filter contentFilter = new ContentFilter(tb1.Text, cli.ID);
            contentFilter.setColor(colpi1.SelectedColor);
            lEval.registerFilter(contentFilter);
            
            clItemL.Add(cli);           
            lb1.Items.Refresh();
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
            cli.ID = generateID();

            Filter soundFilter = new SoundFilter(alertsTB1.Text, cli.ID);
            soundFilter.setColor(alertsColpi.SelectedColor);
            lEval.registerFilter(soundFilter);

            alertsCLItemL.Add(cli);
            alertsLB1.Items.Refresh();
        }

        private void deleteUnCheckedButtonClick(object sender, RoutedEventArgs e)
        {
            lEval.deleteDeactivatedFilters();

            var isNotCheckedItem = clItemL.Where(item => item.IsChecked == false);
            foreach (CheckedListItem c in isNotCheckedItem.ToList()) // .ToList() counters enumeration error! \ (: /
            {
                clItemL.Remove(c);
            }
            lb1.Items.Refresh();
        }

        private void deleteUnCheckedButtonRingClick(object sender, RoutedEventArgs e)
        {
            lEval.deleteDeactivatedFilters();

            var isNotCheckedItem = alertsCLItemL.Where(item => item.IsChecked == false);
            foreach (CheckedListItem c in isNotCheckedItem.ToList()) // .ToList() counters enumeration error! \ (: /
            {
                alertsCLItemL.Remove(c);
            }
            alertsLB1.Items.Refresh();
        }

        private void tb1_GotFocus(object sender, RoutedEventArgs e)
        {
            tb1.Text = "";
        }

        private void alertsTB1_GotFocus(object sender, RoutedEventArgs e)
        {
            alertsTB1.Text = "";
        }

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

            // TODO: Implement clipboard action!
            // ...
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
    }

}

/**
 * RESOURCES
 * - Programmatically add row color: https://stackoverflow.com/questions/10056657/change-wpf-datagrid-row-color
 * 
 */
