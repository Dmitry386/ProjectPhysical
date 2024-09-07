using DVUnityUtilities;
using Packages.DVContainer.Source.Containers;
using Packages.DVContainer.Source.Items;
using Packages.DVContainer.Source.Serialization.Models;
using UnityEngine;

namespace Packages.DVContainer.Source.Serialization
{
    public static class ContainerSerialization
    {
        public static ContainerSerializeData SerializeContainer(Container container)
        {
            var res = new ContainerSerializeData();

            var slots = container.GetSlots();
            res.Slots = new SlotSerializeData[slots.GetLength(0) * slots.GetLength(1)];
            res.Size = new Vector2Int(slots.GetLength(0), slots.GetLength(1));
            res.Owner = container.Owner;

            int id = 0;

            for (int x = 0; x < slots.GetLength(0); x++)
            {
                for (int y = 0; y < slots.GetLength(1); y++)
                {
                    res.Slots[id] = SerializeSlot(slots[x, y], new Vector2Int(x, y));
                    id++;
                }
            }

            return res;
        }

        private static SlotSerializeData SerializeSlot(ContainerSlot slot, Vector2Int pos)
        {
            var res = new SlotSerializeData();
            var slotItems = slot.ItemStack.GetItems();

            res.Items = new ItemSerializeData[slotItems.Length];
            res.Position = pos;

            for (int i = 0; i < slotItems.Length; i++)
            {
                res.Position = pos;
                res.Items[i] = SerializeItem(slotItems[i]);
            }

            return res;
        }

        private static ItemSerializeData SerializeItem(Item item)
        {
            var res = new ItemSerializeData();

            res.Id = item.Id;
            res.Type = item.GetType().Name;
            res.JsonAdditionalData = JsonUtils.ToJson(item);

            return res;
        }
    }
}