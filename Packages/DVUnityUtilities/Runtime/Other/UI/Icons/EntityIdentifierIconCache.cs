using Packages.DVUnityUtilities.Runtime.Other.Identification;
using System;
using System.Linq;
using UnityEngine;

namespace Packages.DVUnityUtilities.Runtime.Other.UI.Icons
{
    public class EntityIdentifierIconCache : MonoBehaviour
    {
        [Serializable]
        public class EntityIconData
        {
            public GameObject ReadIdentifierFrom;
            public string CustomIdentifier;
            public Sprite Icon;
        }

        [SerializeField] private EntityIconData[] _icons = new EntityIconData[0];

        public EntityIconData GetIcon(string identifier)
        {
            for (int i = 0; i < _icons.Length; i++)
            {
                if (_icons[i].ReadIdentifierFrom
                    && _icons[i].ReadIdentifierFrom.TryGetComponent<IEntityIdentifiable>(out var id))
                {
                    if (id.GetIdentifier() == identifier)
                    {
                        return _icons[i];
                    }
                }
            }

            return _icons.FirstOrDefault(x => x.CustomIdentifier == identifier && !x.ReadIdentifierFrom);
        }
    }
}