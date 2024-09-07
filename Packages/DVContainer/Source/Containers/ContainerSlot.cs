using Packages.DVContainer.Source.Items;
using System;

namespace Packages.DVContainer.Source.Containers
{
    [Serializable]
    public class ContainerSlot : IDisposable
    {
        public ItemStack ItemStack = new();

        public void Dispose()
        {
            ItemStack?.Dispose();
        }

        public bool IsEmpty()
        {
            return ItemStack.Count == 0;
        }
    }
}