using Packages.DVContainer.Source.Containers;
using Packages.DVContainer.Source.Events;
using Packages.DVContainer.Source.Mono;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Packages.DVContainer.Source.Views
{
    [DefaultExecutionOrder(-10)]
    public class ContainerGridView : MonoBehaviour
    {
        public event Action<OnItemGridClickedEventArgs> OnClicked;

        [SerializeField]
        private ContainerMono _monoContainer;

        [SerializeField]
        private List<ContainerSlotView> _slots = new();

        private Container _container;

        private void Awake()
        {
            _slots.ForEach(x => x.OnClicked += OnCellClicked);

            if (_monoContainer)
            {
                _container = _monoContainer.GetContainer();
                UpdateVisualization();
            }
        }

        private void OnCellClicked(ContainerSlotView obj)
        {
            OnClicked?.Invoke(new OnItemGridClickedEventArgs()
            {
                GridView = this,
                SlotView = obj
            });
        }

        public void SetContainer(Container c)
        {
            _container = c;
            UpdateVisualization();
        }

        public void UpdateVisualization()
        {
            var slots = _container.GetSlots();
            int slotViewId = 0;

            for (int x = 0; x < slots.GetLength(0); x++)
            {
                for (int y = 0; y < slots.GetLength(1); y++)
                {
                    _slots[slotViewId].SetSlotData(slots[x, y]);

                    slotViewId++;
                }
            }
        }

        private void OnEnable()
        {
            UpdateVisualization();
        }

        private void OnDestroy()
        {
            OnClicked = null;
        }
    }
}