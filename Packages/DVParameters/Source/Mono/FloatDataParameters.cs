using Packages.DVParameters.Source.Parameters;
using UnityEngine;

namespace Packages.DVParameters.Source.Mono
{
    public class FloatDataParameters : MonoBehaviour
    {
        [SerializeField] private bool _debugPrint = true;
        [SerializeField] private DataParameterSerializable<float>[] AwakeParameters;
        public DataParameterContainer<float> DataContainer { get; private set; } = new();

        private void Awake()
        {
            if(_debugPrint)  DataContainer.DebugPrintOwner = name;

            if (AwakeParameters != null)
            {
                DataContainer.SetData(AwakeParameters);
            }
        }

        private void OnDestroy()
        {
            DataContainer.Dispose();
        }
    }
}