using UnityEngine;

namespace Packages.DVUnityUtilities.Runtime.Other.Identification
{
    public class EntityIdentifierMono : MonoBehaviour, IEntityIdentifiable
    {
        [SerializeField] private string _customName;

        string IEntityIdentifiable.GetIdentifier()
        {
            return _customName;
        }

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(_customName))
            {
                _customName = name;
            }
        }
    }
}