using System;

namespace Packages.DVParameters.Source.Parameters
{
    [Serializable]
    public class DataParameterSerializable<T>
    {
        public string Name;
        public T Value;

        public T Min;
        public T Max;
    }
}