using Assets.Scripts.Core.SaveControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.UI.Save
{
    internal class ContinueButtonHiderUI:MonoBehaviour
    {
        [SerializeField] 
        private SaveInventorySystem _saveSystem;

        [SerializeField]
        private GameObject _controlObject;

        private void OnEnable()
        {
            _controlObject.SetActive(_saveSystem.IsHaveSave());
        }
    }
}