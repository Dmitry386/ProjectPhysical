using System;
using UnityEngine;

namespace DVUnityUtilities.Other.PhysicsSystems
{
    [Serializable]
    internal class CollisionIgnoreBetweenLayerInfo
    {
        [Header("Ignore collision between layers")]
        [SerializeField] public int Layer1;
        [SerializeField] public int Layer2;
    }
}