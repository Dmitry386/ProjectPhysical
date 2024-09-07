using Packages.DVContainer.Source.Containers;
using Packages.DVContainer.Source.Items;
using System;

namespace Packages.DVContainer.Source.Events
{
    public class ItemRemovedEventArgs : EventArgs
    {
        public Container Container;
        public Item Item;
    }
}