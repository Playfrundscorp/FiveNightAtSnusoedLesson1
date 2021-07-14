using UnityEngine;

namespace FiveNightsAtSnusoed
{
    public class KeyTabletController : MonoBehaviour
    {
        [SerializeField] private KeyCode _tabletUseKey = KeyCode.Tab;
        [SerializeField] private Tablet _tablet;

        private void Update()
        {
            if (Input.GetKeyDown(_tabletUseKey))
            {
                if (_tablet.IsShown)
                {
                    _tablet.Hide();
                }
                else
                {
                    _tablet.Show();
                }
            }
        }
    } 
}
