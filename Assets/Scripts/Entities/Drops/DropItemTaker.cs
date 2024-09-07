using Packages.DVContainer.Source.Containers;
using Packages.DVContainer.Source.Mono;
using UnityEngine;

namespace Assets.Scripts.Entities.Drops
{
    internal class DropItemTaker : MonoBehaviour
    {
        [SerializeField]
        private ContainerMono _containerMono;

        private Container _container;

        private void Awake()
        {
            _container = _containerMono.GetContainer();
        }

        public void TakeItem(DroppedItem droppedItem)
        {
            if (_container.AddItem(Instantiate(droppedItem.GetItem())))
            {
                GameObject.Destroy(droppedItem.gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<DroppedItem>(out var dropped))
            {
                TakeItem(dropped);
            }
        }
    }
}