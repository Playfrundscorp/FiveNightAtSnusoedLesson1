using UnityEngine;

namespace FiveNightsAtSnusoed
{
    public abstract class Tablet : MonoBehaviour
    {
        private bool _isShown = false;
        public bool IsShown { get => _isShown; }

        public abstract void PickCamera(string locationName);

        public void Show()
        {
            _isShown = true;
            OnShown();
        }
        public void Hide() 
        {
            _isShown = false;
            OnHidden();
        }

        protected abstract void OnShown();
        protected abstract void OnHidden();

    }

}