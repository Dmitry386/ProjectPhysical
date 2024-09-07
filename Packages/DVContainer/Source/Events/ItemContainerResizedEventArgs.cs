using Packages.DVContainer.Source.Containers;
using System;
using UnityEngine;

namespace Packages.DVContainer.Source.Events
{
    public class ItemContainerResizedEventArgs : EventArgs
    {
        public Container Container;
        public Vector2Int OldSize;
        public Vector2Int NewSize;
    }
}