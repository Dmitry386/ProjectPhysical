using Packages.DVContainer.Source.Events;
using Packages.DVContainer.Source.Items;
using System;
using UnityEngine;

namespace Packages.DVContainer.Source.Containers
{
    public class Container
    {
        public event Action<ItemAddedEventArgs> OnItemAdded;

        public event Action<ItemRemovedEventArgs> OnItemRemoved;

        public event Action<ItemContainerResizedEventArgs> OnItemContainerResized;

        public string Owner { get; }

        private ContainerSlot[,] _slots;

        public Container(string owner, int width, int height)
        {
            Owner = owner;
            SetSize(width, height);
        }

        public void SetSize(int width, int height)
        {
            Vector2Int oldSize = new Vector2Int(0, 0);
            if (_slots != null) oldSize = new Vector2Int(_slots.GetLength(0), _slots.GetLength(1));

            var newSlots = new ContainerSlot[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (_slots != null) // copy old
                    {
                        newSlots[x, y] = _slots[x, y];
                    }
                    else
                    {
                        newSlots[x, y] = new ContainerSlot();
                    }
                }
            }

            _slots = newSlots;

            OnItemContainerResized?.Invoke(new ItemContainerResizedEventArgs()
            {
                Container = this,
                OldSize = oldSize,
                NewSize = new Vector2Int(width, height)
            });
        }

        public bool AddItem(Item item)
        {
            // add to first with same type
            for (int x = 0; x < _slots.GetLength(0); x++)
            {
                for (int y = 0; y < _slots.GetLength(1); y++)
                {
                    if (_slots[x, y].ItemStack.GetStackItemId() == item.Id
                        && _slots[x, y].ItemStack.AddItem(item))
                    {
                        return true;
                    }
                }
            }

            // add to first free
            for (int x = 0; x < _slots.GetLength(0); x++)
            {
                for (int y = 0; y < _slots.GetLength(1); y++)
                {
                    if (_slots[x, y].ItemStack.IsPossibleToAdd(item.Id, item.MaxStackSize, 1)
                        && AddItem(x, y, item))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool AddItem(int x, int y, Item item)
        {
            var slot = _slots[x, y];

            if (slot.ItemStack.IsPossibleToAdd(item.Id, item.MaxStackSize, 1))
            {
                slot.ItemStack.AddItem(item);
                OnItemAdded?.Invoke(new ItemAddedEventArgs() { Container = this, Item = item });

                return true;
            }

            return false;
        }

        public bool RemoveItem(int x, int y, Item item)
        {
            var slot = _slots[x, y];

            slot.ItemStack.RemoveItem(item);
            OnItemRemoved?.Invoke(new ItemRemovedEventArgs() { Container = this, Item = item });

            return true;
        }

        public bool RemoveItem(Item item)
        {
            var slot = GetSlotWithItem(item);
            if (slot == null) return false;

            slot.ItemStack.RemoveItem(item);
            OnItemRemoved?.Invoke(new ItemRemovedEventArgs() { Container = this, Item = item });

            return true;
        }

        private ContainerSlot GetSlotWithItem(Item item)
        {
            for (int x = 0; x < _slots.GetLength(0); x++)
            {
                for (int y = 0; y < _slots.GetLength(1); y++)
                {
                    if (_slots[x, y].ItemStack.GetStackItemId() == item.Id)
                    {
                        return _slots[x, y];
                    }
                }
            }
            return null;
        }

        public bool IsEmptySlot(int x, int y)
        {
            return _slots[x, y].IsEmpty();
        }

        public ContainerSlot[,] GetSlots()
        {
            return _slots;
        }

        public void SetSlots(ContainerSlot[,] slots)
        {
            _slots = slots;

            OnItemContainerResized?.Invoke(new ItemContainerResizedEventArgs()
            {
                Container = this,
                NewSize = new Vector2Int(_slots.GetLength(0), _slots.GetLength(1)),
                OldSize = new Vector2Int()
            });
        }

        public void Clear()
        {
            for (int x = 0; x < _slots.GetLength(0); x++)
            {
                for (int y = 0; y < _slots.GetLength(1); y++)
                {
                    _slots[x, y].ItemStack.ClearItems();
                }
            }
        }
    }
}