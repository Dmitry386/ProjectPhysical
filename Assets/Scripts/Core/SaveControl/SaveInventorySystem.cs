using DVUnityUtilities;
using Packages.DVContainer.Source.Mono;
using Packages.DVContainer.Source.Serialization;
using Packages.DVContainer.Source.Serialization.Models;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.Core.SaveControl
{
    internal class SaveInventorySystem : MonoBehaviour
    {
        [SerializeField]
        private string _filePath = "inventory.json";

        [SerializeField]
        private ContainerMono _containerToSaveLoad;

        public void Save()
        {
            var toSave = ContainerSerialization.SerializeContainer(_containerToSaveLoad.GetContainer());
            string json = JsonUtils.ToFile(toSave, GetPath());
            DebugUtils.Log(this, $"Inventory saved: {json}");
        }

        public async void Load()
        {
            if (!_containerToSaveLoad) return;

            string json = await JsonUtils.ReadFile(GetPath());
            var loaded = JsonUtils.FromJsonOrNew<ContainerSerializeData>(json);
            var newContainer = ContainerDeserialization.DeserializeContainer(loaded);
            _containerToSaveLoad.GetContainer().SetSlots(newContainer.GetSlots());
            DebugUtils.Log(this, $"Inventory loaded: {json}");
        }

        public string GetPath()
        {
            return Path.Combine(Application.persistentDataPath, _filePath);
        }

        public bool IsHaveSave()
        {
            return File.Exists(GetPath());
        }
    }
}