using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkID=390556 上有介绍

namespace TestDemo
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class ListViewItemDemo : Page
    {
        public ListViewItemDemo()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //this.sbShowListViewItem.Begin();
            // ATTN: Please note it's a "TrulyObservableCollection" that's instantiated. Otherwise, "Trades[0].Qty = 999" will NOT trigger event handler "Trades_CollectionChanged" in main.
            // REF: http://stackoverflow.com/questions/8490533/notify-observablecollection-when-item-changes
            TrulyObservableCollection<Trade> Trades = new TrulyObservableCollection<Trade>();
            Trades.Add(new Trade { Symbol = "APPL", Qty = 123 });
            Trades.Add(new Trade { Symbol = "IBM", Qty = 456 });
            Trades.Add(new Trade { Symbol = "CSCO", Qty = 789 });

            Trades.CollectionChanged += Trades_CollectionChanged;
            //Trades.ItemPropertyChanged += PropertyChangedHandler;
            Trades.RemoveAt(2);

            Trades[0].Qty = 999;
        }
        static void PropertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            //Console.WriteLine(DateTime.Now.ToString() + ", Property changed: " + e.PropertyName + ", Symbol: " + ((Trade)sender).Symbol + ", Qty: " + ((Trade)sender).Qty);
            return;
        }

        static void Trades_CollectionChanged(object sender, EventArgs e)
        {
            //Console.WriteLine(DateTime.Now.ToString() + ", Collection changed");
            return;
        }
    }
    class Trade : INotifyPropertyChanged
    {
        protected string _Symbol;
        protected int _Qty = 0;
        protected DateTime _OrderPlaced = DateTime.Now;

        public DateTime OrderPlaced
        {
            get { return _OrderPlaced; }
        }

        public string Symbol
        {
            get
            {
                return _Symbol;
            }
            set
            {
                _Symbol = value;
                NotifyPropertyChanged("Symbol");
            }
        }

        public int Qty
        {
            get
            {
                return _Qty;
            }
            set
            {
                _Qty = value;
                NotifyPropertyChanged("Qty");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
