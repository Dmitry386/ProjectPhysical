using Assets.Scripts.Entities.Items;
using Assets.Scripts.Gameplay.PlayerSystem;
using Packages.DVContainer.Source.Containers;
using Packages.DVContainer.Source.Items;
using Packages.DVContainer.Source.Mono;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI.Inventory
{
    internal class ContainerActionsMenuUI : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _selectedText;

        private PlayerEntity _player;
        private ContainerSlot _itemSlot;

        private Item _firstItem => _itemSlot.ItemStack.GetItems()[0];

        public void SetData(ContainerSlot item, PlayerEntity user)
        {
            _itemSlot = item;
            _player = user;

            if (_itemSlot != null && !_itemSlot.IsEmpty())
            {
                _selectedText.text = _firstItem.GetDisplayName();
            }
            else
            {
                _selectedText.text = string.Empty;
            }
        }

        private void UpdateChecks()
        {
            if (_itemSlot != null)
            {
                if (_itemSlot.IsEmpty())
                {
                    SetData(null, null);
                }
            }
        }

        public void Drop()
        {
            if (_player && _itemSlot != null && !_itemSlot.IsEmpty())
            {
                _player.GetComponent<ContainerMono>()?.GetContainer()?.RemoveItem(_firstItem);
            }
            UpdateChecks();
        }

        public void Use()
        {
            if (_player && _itemSlot != null && !_itemSlot.IsEmpty())
            {
                if (_firstItem is BaseUsableItem usable)
                {
                    usable.Use(_player);
                }
            }
            UpdateChecks();
        }

        private void OnEnable()
        {
            SetData(null, null);
        }
    }
}