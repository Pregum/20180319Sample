using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace _20180319Sample
{
    public class CollectionBasedDictionary<TKey, TValue> : KeyedCollection<TKey, KeyValuePair<TKey, TValue>>
    {
        protected override TKey GetKeyForItem(KeyValuePair<TKey, TValue> item)
        {
            return item.Key;
        }
    }

    public class ObservableDictionary<TKey, TValue> : CollectionBasedDictionary<TKey, TValue>, INotifyPropertyChanged,
        INotifyCollectionChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add => PropertyChanged += value;
            remove => PropertyChanged -= value;
        }

        protected readonly string IndexerName = "Item[]";
        protected readonly string CountName = "Count";

        protected override void InsertItem(int index, KeyValuePair<TKey, TValue> item)
        {
            base.InsertItem(index, item);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
            OnPropertyChanged(IndexerName);
            OnPropertyChanged(CountName);
        }

        protected override void RemoveItem(int index)
        {
            var item = this[index];
            base.RemoveItem(index);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));
            OnPropertyChanged(IndexerName);
            OnPropertyChanged(CountName);
        }

        protected override void SetItem(int index, KeyValuePair<TKey, TValue> item)
        {
            var oldItem = this[index];
            base.SetItem(index, item);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, item, oldItem, index));
            OnPropertyChanged(IndexerName);
            //OnPropertyChanged(CountName);
        }

        protected override void ClearItems()
        {
            base.ClearItems();
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            OnPropertyChanged(IndexerName);
            OnPropertyChanged(CountName);
        }

        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(this, e);
        }

        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

}