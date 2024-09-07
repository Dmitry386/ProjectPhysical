using System;
using UnityEngine;

namespace Packages.DVContainer.Source.Serialization.Models
{
    [Serializable]
    public class SlotSerializeData
    {
        public Vector2Int Position;
        public ItemSerializeData[] Items = new ItemSerializeData[0];
    }
}