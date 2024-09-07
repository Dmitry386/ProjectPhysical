using System;
using System.Collections.Generic;

namespace Packages.DVContainer.Source.Items
{
    [Serializable]
    public class ItemStack : IDisposable
    {
        public Action<ItemStack> OnUpdated;

        private List<Item> _items = new();

        public int Count => _items.Count;

        public ItemStack(params Item[] items)
        {
            _items.AddRange(items);
        }

        public string Id
        {
            get
            {
                if (_items.Count == 0) return string.Empty;
                return _items[0].Id;
            }
        }

        public bool AddItem(Item item)
        {
            if (!IsPossibleToAdd(item.Id, item.MaxStackSize, 1) || _items.Contains(item)) return false;
            _items.Add(item);

            OnUpdated?.Invoke(this);
            return true;
        }

        public string GetStackItemId()
        {
            if (_items.Count == 0) return string.Empty;
            return _items[0].Id;
        }

        public bool IsPossibleToAdd(string itemId, int maxStackSize, int countToAdd)
        {
            if (_items.Count == 0)
            {
                return true;
            }

            return GetStackItemId() == itemId && Count + countToAdd <= maxStackSize;
        }

        public void RemoveItem(Item item)
        {
            _items.Remove(item);
            OnUpdated?.Invoke(this);
        }

        public Item[] GetItems()
        {
            return _items.ToArray();
        }

        public void ClearItems()
        {
            _items.Clear();
            OnUpdated?.Invoke(this);
        }

        public void Dispose()
        {
            OnUpdated = null;
            _items.Clear();
        }
         
    }
}