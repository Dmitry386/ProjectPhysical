using Packages.DVContainer.Source.Containers;
using UnityEngine;

namespace Packages.DVContainer.Source.Mono
{
    public class ContainerMono : MonoBehaviour
    {
        [SerializeField]
        private Vector2Int _size;

        [SerializeField]
        private string _ownerName;

        private Container _container;

        protected virtual void Awake()
        {
            InitContainer();
        }

        public Container GetContainer()
        {
            InitContainer();
            return _container;
        }

        protected void InitContainer()
        {
            if (_container == null)
            {
                _container = new Container(_ownerName, _size.x, _size.y);
            }
        }

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(_ownerName))
            {
                _ownerName = name;
            }
        }
    }
}