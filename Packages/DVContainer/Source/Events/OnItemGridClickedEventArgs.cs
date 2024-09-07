using Packages.DVContainer.Source.Items;
using Packages.DVContainer.Source.Views;
using System;

namespace Packages.DVContainer.Source.Events
{
    public class OnItemGridClickedEventArgs : EventArgs
    {
        public ContainerGridView GridView;
        public ContainerSlotView SlotView;
        public Item Item;
    }
}