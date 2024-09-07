using DVUnityUtilities;
using DVUnityUtilities.Other.LongActions;
using DVUnityUtilities.Other.UI.ProgressBar;
using UnityEngine;

namespace Packages.DVUnityUtilities.Runtime.Other.LongActions
{
    public class LongActionUI : MonoBehaviour
    {
        [SerializeField] private GameObject _hideObject;
        [SerializeField] private LongActionSystem _user;
        [SerializeField] private ProgressBarUI _progressBar;
         
        private void Update()
        {
            _hideObject.SetActive(_user.GetDuration() > 0);
            _progressBar.SetValue(PercentUtils.HowManyPercentValueOfValue01(_user.GetTimeElapsed(), _user.GetDuration()));
        }
    }
}