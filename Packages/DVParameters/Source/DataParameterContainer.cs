using Packages.DVParameters.Source.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Packages.DVParameters.Source
{
    [Serializable]
    public class DataParameterContainer<T> : IDisposable
    {
        public event Action<DataParameterContainer<T>> OnChanged;
        public string DebugPrintOwner { get; set; }
        private Dictionary<string, DataParameterSerializable<T>> _cachedParameters = new();

        public void SetData(string name, T val)
        {
            if (GetData(name, out var r))
            {
                val = (T)Clamp(val, r.Max, r.Min);

                if (DebugPrintOwner != null) Debug.Log($"[DPC] Entity: [{DebugPrintOwner}]. Parameter [{name}] set value = [{val}]. Old value = [{r.Value}]");
                r.Value = val;

                OnChanged?.Invoke(this);
            }
            else
            {
                _cachedParameters.Add(name, new DataParameterSerializable<T>() { Name = name, Value = val });
                OnChanged?.Invoke(this);
                if (DebugPrintOwner != null) Debug.Log($"[DPC] Entity: [{DebugPrintOwner}]. Parameter [{name}] first set value = [{val}]");
            }
        }

        public void SetData(DataParameterSerializable<T> p)
        {
            if (GetData(p.Name, out var r))
            {
                if (DebugPrintOwner != null) Debug.Log($"[DPC] Entity: [{DebugPrintOwner}]. Parameter [{r.Name}] set value = [{p.Value}]. Old value = [{r.Value}]");

                r.Value = p.Value;
                r.Max = p.Max;
                r.Min = p.Min;

                OnChanged?.Invoke(this);
            }
            else
            {
                _cachedParameters.Add(p.Name, new DataParameterSerializable<T>()
                {
                    Name = p.Name,
                    Value = p.Value,
                    Max = p.Max,
                    Min = p.Min,
                });

                OnChanged?.Invoke(this);
                if (DebugPrintOwner != null) Debug.Log($"[DPC]  Entity: [{DebugPrintOwner}]. Parameter [{p.Name}] first set value = [{p.Value}]");
            }
        }

        public static object Clamp(object value, object max, object min)
        {
            if (value is IComparable c_Value
                && max is IComparable c_Max
                && min is IComparable c_Min)
            {
                if (c_Value.CompareTo(c_Max) > 0) return c_Max;
                if (c_Value.CompareTo(c_Min) < 0) return c_Min;
            }

            return value;
        }

        public void SetData(IEnumerable<DataParameterSerializable<T>> parameters)
        {
            foreach (var p in parameters)
            {
                SetData(p);
            }

            OnChanged?.Invoke(this);
        }

        public void RemoveData(DataParameterSerializable<T> param)
        {
            _cachedParameters.Remove(param.Name);
            OnChanged?.Invoke(this);
        }

        public bool GetData(string name, out DataParameterSerializable<T> parameter)
        {
            if (_cachedParameters.TryGetValue(name, out parameter))
            {
                return true;
            }
            return false;
        }

        public void Clear()
        {
            _cachedParameters.Clear();
            OnChanged?.Invoke(this);
        }

        public DataParameterSerializable<T>[] GetAll()
        {
            return _cachedParameters.Values.ToArray();
        }

        public void Dispose()
        {
            _cachedParameters.Clear();
            OnChanged = null;
        }
    }
}