using UnityEngine;

namespace Packages.DVUnityUtilities.Runtime.Other.Identification
{
    internal class EntityIdentifierFromName : MonoBehaviour, IEntityIdentifiable
    {
        string IEntityIdentifiable.GetIdentifier()
        {
            return name;
        }
    }
}