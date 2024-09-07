using System;
using UnityEngine;

namespace Packages.DVContainer.Source.Items
{
    [Serializable]
    public abstract class Item : ScriptableObject
    {
        [SerializeField]
        public string Id;

        [SerializeField]
        private string _displayName;

        [SerializeField]
        private int _maxStackSize = 64;

        public int MaxStackSize => _maxStackSize;

        protected void SetMaxStackSize(int maxSize)
        {
            _maxStackSize = maxSize;
        }

        public string GetDisplayName()
        {
            if(string.IsNullOrEmpty(_displayName))
            {
                return Id;
            }
            else
            {
                return _displayName;
            }
        }
    }
}