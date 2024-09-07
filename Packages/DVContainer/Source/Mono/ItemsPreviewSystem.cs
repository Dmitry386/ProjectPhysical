using Packages.DVContainer.Source.Items;
using Packages.DVContainer.Source.Mono.Info;
using System.Collections.Generic;
using UnityEngine;

namespace Packages.DVContainer.Source.Mono
{
    public class ItemsPreviewSystem : MonoBehaviour
    {
        [SerializeField]
        private ItemPreviewsConfig _cfg;

        private Dictionary<string, Sprite> _sprites = new();

        private void Awake()
        {
            foreach (var item in _cfg.ItemPreviews)
            {
                _sprites.Add(item.Item.Id, item.Icon);
            }
        }

        public bool TryGetPreview(Item item, out Sprite preview)
        {
            return _sprites.TryGetValue(item.Id, out preview);
        }
    }
}
