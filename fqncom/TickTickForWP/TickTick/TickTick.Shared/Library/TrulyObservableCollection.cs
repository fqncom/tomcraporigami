using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;

namespace TickTick.Library
{
    public class TrulyObservableCollection<T> : ObservableCollection<T> where T : INotifyPropertyChanged
    {
        public TrulyObservableCollection()
            : base()
        {
            HookupCollectionChangedEvent();
        }

        public TrulyObservableCollection(IEnumerable<T> collection)
            : base(collection)
        {
            foreach (T item in collection)
            {
                item.PropertyChanged += ItemPropertyChanged;
            }

            HookupCollectionChangedEvent();
        }

        public TrulyObservableCollection(List<T> list)
            : base(list)
        {
            //list.ForEach(item => item.PropertyChanged += ItemPropertyChanged);
            foreach (T item in list)
            {
                item.PropertyChanged += ItemPropertyChanged;
            }

            HookupCollectionChangedEvent();
        }

        private void HookupCollectionChangedEvent()
        {
            CollectionChanged += new NotifyCollectionChangedEventHandler(TrulyObservableCollectionChanged);
        }

        private void TrulyObservableCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Object item in e.NewItems)
                {
                    ((INotifyPropertyChanged)item).PropertyChanged += ItemPropertyChanged;
                }
            }
            if (e.OldItems != null)
            {
                foreach (Object item in e.OldItems)
                {
                    ((INotifyPropertyChanged)item).PropertyChanged -= ItemPropertyChanged;
                }
            }
        }

        private void ItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyCollectionChangedEventArgs args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, sender, sender, IndexOf((T)sender));
            OnCollectionChanged(args);
        }
    }
}
