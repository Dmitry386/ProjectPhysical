using Packages.DVContainer.Source.Items;
using UnityEngine;

namespace Assets.Scripts.Entities.Drops
{
    internal class DroppedItem : MonoBehaviour
    {
        [SerializeField]
        private Item _item;

        public void SetItem(Item item)
        {
            _item = item;
        }

        public Item GetItem()
        {
            return _item;
        }
    }
}