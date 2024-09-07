using Assets.Scripts.Gameplay.PlayerSystem;
using Packages.DVContainer.Source.Events;
using Packages.DVContainer.Source.Views;
using UnityEngine;

namespace Assets.Scripts.UI.Inventory
{
    internal class ContainerActionsController : MonoBehaviour
    {
        [SerializeField]
        private PlayerEntity _player;

        [SerializeField]
        private ContainerGridView _gridView;

        [SerializeField]
        private ContainerActionsMenuUI _actionsMenu;

        private void Awake()
        {
            _gridView.OnClicked += OnCellClicked;
        }

        private void OnCellClicked(OnItemGridClickedEventArgs obj)
        {
            var slot = obj.SlotView.GetSlotData();

            if (!slot.IsEmpty())
            {
                _actionsMenu.SetData(slot, _player);
            }
            else
            {
                _actionsMenu.SetData(null, null);
            }
        }

        private void OnDestroy()
        {
            _gridView.OnClicked -= OnCellClicked;
        }
    }
}