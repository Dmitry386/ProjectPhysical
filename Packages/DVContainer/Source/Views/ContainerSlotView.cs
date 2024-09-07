using DVUnityUtilities.Other.ServiceLocator;
using Packages.DVContainer.Source.Containers;
using Packages.DVContainer.Source.Items;
using Packages.DVContainer.Source.Mono;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Packages.DVContainer.Source.Views
{
    public class ContainerSlotView : MonoBehaviour, IPointerClickHandler
    {
        public event Action<ContainerSlotView> OnClicked;

        [SerializeField]
        private Image _img;

        [SerializeField]
        private TMP_Text _textName;

        [SerializeField]
        private TMP_Text _textCount;

        private ContainerSlot _containerSlot;

        public void SetSlotData(ContainerSlot containerSlot)
        {
            if (_containerSlot != null) _containerSlot.ItemStack.OnUpdated -= OnUpdated;
            if (containerSlot != null) containerSlot.ItemStack.OnUpdated += OnUpdated;

            _containerSlot = containerSlot;

            UpdateVisualization();
        }

        private void OnUpdated(ItemStack stack)
        {
            UpdateVisualization();
        }

        private void UpdateVisualization()
        {
            if (_textCount)
            {
                if (_containerSlot != null && _containerSlot.ItemStack.Count > 0)
                {
                    _textCount.text = _containerSlot.ItemStack.Count.ToString();
                }
                else
                {
                    _textCount.text = string.Empty;
                }
            }

            if (_textName && _containerSlot != null && _containerSlot.ItemStack.Count > 0 && _containerSlot.ItemStack != null)
            {
                _textName.text = _containerSlot.ItemStack.GetItems()[0].GetDisplayName();
            }
            else
            {
                _textName.text = string.Empty;
            }

            if (_img)
            {
                var previewService = DVServiceLocator.GetService<ItemsPreviewSystem>();
                if (previewService)
                {
                    if (_containerSlot != null && _containerSlot.ItemStack.Count > 0)
                    {

                        if (previewService.TryGetPreview(_containerSlot.ItemStack.GetItems()[0], out Sprite preview))
                        {
                            _img.sprite = preview;
                            _img.enabled = true;

                        }
                        else
                        {
                            _img.enabled = false;
                        }
                    }
                    else
                    {
                        _img.enabled = false;
                    }
                }
            }
        }

        public void SetIcon(Sprite sprite)
        {
            if (_img)
            {
                _img.sprite = sprite;
            }
        }

        public ContainerSlot GetSlotData()
        {
            return _containerSlot;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClicked?.Invoke(this);
        }

        private void OnDestroy()
        {
            if (_containerSlot != null) _containerSlot.ItemStack.OnUpdated -= OnUpdated;
            OnClicked = null;
            _containerSlot.Dispose();
        }
    }
}