using System;
using System.Collections.Generic;
using UnityEngine;

namespace Packages.DVContainer.Source.Serialization.Models
{
    [Serializable]
    public class ContainerSerializeData
    {
        public string Owner;
        public Vector2Int Size;
        public SlotSerializeData[] Slots = new SlotSerializeData[0];
    }
}
