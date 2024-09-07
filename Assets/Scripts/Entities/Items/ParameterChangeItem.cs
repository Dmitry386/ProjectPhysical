using Assets.Scripts.Gameplay.PlayerSystem;
using Packages.DVParameters.Source.Mono;
using UnityEngine;

namespace Assets.Scripts.Entities.Items
{
    internal class ParameterChangeItem : BaseUsableItem
    {
        [SerializeField]
        private float _addValue = 1;

        [SerializeField]
        private string _parameterName = "Health";

        public override bool Use(PlayerEntity player)
        {
            if (player.TryGetComponent<FloatDataParameters>(out var dataParam))
            {
                if (dataParam.DataContainer.GetData(_parameterName, out var parameter))
                {
                    dataParam.DataContainer.SetData(_parameterName, parameter.Value + _addValue);
                    RemoveFromContainer(player);
                    return true;
                }
            }
            return false;
        }
    }
}
