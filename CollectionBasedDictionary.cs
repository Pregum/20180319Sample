using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace _20180319Sample
{
    public class CollectionBasedDictionary<TKey, TValue> : KeyedCollection<TKey, KeyValuePair<TKey, TValue>>,
        IDictionary<TKey, TValue>
    {
        public CollectionBasedDictionary()
            : base()
        {
        }

        public CollectionBasedDictionary(IDictionary<TKey, TValue> dictionary)
            : base()
        {
            foreach (var kvp in dictionary)
            {
                Add(kvp);
            }
        }

        public CollectionBasedDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
            : base(comparer)
        {
            foreach (var kvp in dictionary)
            {
                Add(kvp);
            }
        }

        protected override TKey GetKeyForItem(KeyValuePair<TKey, TValue> item)
        {
            return item.Key;
        }

        public bool ContainsKey(TKey key)
        {
            return this.Dictionary.ContainsKey(key);
        }

        public void Add(TKey key, TValue value)
        {
            //this.Add(new KeyValuePair<TKey, TValue>(key, value));
            var kvp = new KeyValuePair<TKey, TValue>(key, value);
            Add(kvp);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            KeyValuePair<TKey, TValue> kvp;
            var result = this.Dictionary.TryGetValue(key, out kvp);

            value = kvp.Value;
            return result;
        }

        public new TValue this[TKey key]
        {
            get
            {
                if (Dictionary == null)
                {
                    throw new KeyNotFoundException("指定されたキーはディクショナリ内に存在しませんでした。");
                }

                return Dictionary[key].Value;
            }
            set
            {
                KeyValuePair<TKey, TValue> newValue = new KeyValuePair<TKey, TValue>(key, value);
                KeyValuePair<TKey, TValue> oldValue;
                if (Dictionary.TryGetValue(key, out oldValue))
                {
                    var index = Items.IndexOf(oldValue);
                    SetItem(index, newValue);
                }
                else
                {
                    InsertItem(Count, newValue);
                }
            }
        }

        public ICollection<TKey> Keys => Dictionary?.Keys ?? Enumerable.Empty<TKey>().ToArray();

        public ICollection<TValue> Values => Dictionary?.Values
                                                 .Select(x => x.Value)
                                                 .ToArray() ?? Enumerable.Empty<TValue>().ToArray();
    }

    public class ObservableDictionary<TKey, TValue> : CollectionBasedDictionary<TKey, TValue>,
        INotifyPropertyChanged,
        INotifyCollectionChanged
    {
        public ObservableDictionary()
            : base()
        {
        }

        public ObservableDictionary(IDictionary<TKey, TValue> dictionary)
            : base(dictionary)
        {
        }

        public ObservableDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
            : base(dictionary, comparer)
        {
        }

        private readonly string ItemsName = "Items[]";
        private readonly string CountName = "Count";
        private readonly string DictionaryName = "Dictionary";
        private readonly string KeysName = "Keys[]";
        private readonly string ValuesName = "Values[]";


        public event PropertyChangedEventHandler PropertyChanged;
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add { PropertyChanged += value; }
            remove { PropertyChanged -= value; }
        }

        private void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(this, e);
        }

        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void OnCollectionAdded(KeyValuePair<TKey, TValue> changedItem, int startingIndex)
        {
            OnPropertyChanged(DictionaryName);
            OnPropertyChanged(ItemsName);
            OnPropertyChanged(CountName);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, changedItem,
                startingIndex));
        }

        private void OnCollectionRemoved(KeyValuePair<TKey, TValue> changedItem, int startingIndex)
        {
            OnPropertyChanged(DictionaryName);
            OnPropertyChanged(ItemsName);
            OnPropertyChanged(KeysName);
            OnPropertyChanged(ValuesName);
            OnPropertyChanged(CountName);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, changedItem,
                startingIndex));
        }

        private void OnCollectionMoved(KeyValuePair<TKey, TValue> changedItem, int index, int oldIndex)
        {
            OnPropertyChanged(DictionaryName);
            OnPropertyChanged(ItemsName);
            OnPropertyChanged(KeysName);
            OnPropertyChanged(ValuesName);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Move, changedItem,
                index, oldIndex));
        }

        private void OnCollectionReplaced(KeyValuePair<TKey, TValue> newItem, KeyValuePair<TKey, TValue> oldItem)
        {
            OnPropertyChanged(DictionaryName);
            OnPropertyChanged(ItemsName);
            OnPropertyChanged(KeysName);
            OnPropertyChanged(ValuesName);
            OnCollectionChanged(
                new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, newItem, oldItem));
        }

        private void OnCollectionReset()
        {
            OnPropertyChanged(DictionaryName);
            OnPropertyChanged(ItemsName);
            OnPropertyChanged(KeysName);
            OnPropertyChanged(ValuesName);
            OnPropertyChanged(CountName);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        protected override void SetItem(int index, KeyValuePair<TKey, TValue> item)
        {
            KeyValuePair<TKey, TValue> oldItem = this[index];
            base.SetItem(index, item);
            OnCollectionReplaced(item, oldItem);
        }

        protected override void InsertItem(int index, KeyValuePair<TKey, TValue> item)
        {
            base.InsertItem(index, item);
            OnCollectionAdded(item, index);
        }

        protected override void RemoveItem(int index)
        {
            KeyValuePair<TKey, TValue> removedItem = this[index];
            base.RemoveItem(index);
            OnCollectionRemoved(removedItem, index);
        }

        protected override void ClearItems()
        {
            base.ClearItems();
            OnCollectionReset();
        }
    }
}