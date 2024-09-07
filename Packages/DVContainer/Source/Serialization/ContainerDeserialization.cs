using DVUnityUtilities;
using Packages.DVContainer.Source.Containers;
using Packages.DVContainer.Source.Items;
using Packages.DVContainer.Source.Serialization.Models;
using System;
using System.Linq;
using UnityEngine;

namespace Packages.DVContainer.Source.Serialization
{
    public static class ContainerDeserialization
    {
        public static Container DeserializeContainer(ContainerSerializeData data)
        {
            var res = new Container(data.Owner, data.Size.x, data.Size.y);
            res.SetSlots(DeserializeSlots(data));

            return res;
        }

        private static ContainerSlot[,] DeserializeSlots(ContainerSerializeData data)
        {
            var res = new ContainerSlot[data.Size.x, data.Size.y];
            SlotSerializeData slot;

            for (int i = 0; i < data.Slots.Length; i++)
            {
                slot = data.Slots[i];
                res[slot.Position.x, slot.Position.y] = DeserializeSlot(slot);
            }

            return res;
        }

        private static ContainerSlot DeserializeSlot(SlotSerializeData slot)
        {
            var res = new ContainerSlot();
            var items = new Item[slot.Items.Length];

            for (int i = 0; i < items.Length; i++)
            {
                items[i] = DeserializeItem(slot.Items[i]);
                if (items[i] != null) res.ItemStack.AddItem(items[i]);
            }

            return res;
        }

        private static Item DeserializeItem(ItemSerializeData itemSerializeData)
        {
            var type = TypeFromString(itemSerializeData.Type);
            if (type == null)
            {
                DebugUtils.LogWarning(itemSerializeData, $"Type: [{itemSerializeData.Type}] not found");
                return null;
            }

            var allItems = Resources.LoadAll(string.Empty, type);

            Item scriptableObjectItem = null;

            foreach (var item in allItems)
            {
                if (item is Item i
                    && i.Id == itemSerializeData.Id)
                {
                    scriptableObjectItem = i;
                    break;
                }
            }

            if (scriptableObjectItem != null)
            {
                Item item = GameObject.Instantiate(scriptableObjectItem);
                JsonUtility.FromJsonOverwrite(itemSerializeData.JsonAdditionalData, item);

                return item;
            }

            return null;
        }

        private static Type TypeFromString(string type)
        {
            return System.AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).First(x => x.Name == type); ;
        }
    }
}