using System.Collections.Generic;
using UnityEngine;

namespace Packages.DVContainer.Source.Mono.Info
{
    public class ItemPreviewsConfig : ScriptableObject
    {
        [SerializeField]
        public List<ItemPreviewInfo> ItemPreviews = new();
    }
}
