using System.Collections.Generic;

namespace Packages.DVUnityUtilities.Runtime.Other.Blocking
{
    public class BlockerList
    {
        private List<object> _blockers = new();

        public void AddBlocker(object obj)
        {
            if (_blockers.Contains(obj)) return;
            _blockers.Add(obj);
        }

        public void RemoveBlocker(object obj)
        {
            _blockers.Remove(obj);
        }

        public void Clear()
        {
            _blockers.Clear();
        }

        public bool IsBlocked()
        {
            return _blockers.Count > 0;
        }
    }
}