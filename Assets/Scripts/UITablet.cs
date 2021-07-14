using System;
using UnityEngine;

namespace FiveNightsAtSnusoed
{
    
    public class UITablet : Tablet
    {
        [Serializable]
        private class TabletSounds 
        {
            public AudioClip Show;
            public AudioClip Hide;
            public AudioClip ChangeCamera;
        }
        [Header("Sounds")]
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private TabletSounds _tabletSounds;

        [Header("Controls")]
        [SerializeField] private string _startLocationName;
        [SerializeField] private LocationsContainer _locationsContainer;
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private Canvas _tabletCanvas;

        private string _currentLocation;
        private float _cameraStartFarClipPlane;

        private void Awake()
        {
            if (!_locationsContainer.ContainLocationWithName(_startLocationName))
                throw new ArgumentException($"{nameof(_locationsContainer)} does not contain a {nameof(Location)} " +
                    $"named {_startLocationName} in {nameof(_startLocationName)} field!");
            
            _currentLocation = _startLocationName;
            _cameraStartFarClipPlane = _mainCamera.farClipPlane;

            _tabletCanvas.enabled = false;
            DisableAllCameras();          
        }

        public override void PickCamera(string locationName) 
        {
            if (IsShown)
            {
                Location location;
                if (_locationsContainer.TryFindByName(locationName, out location))
                {
                    DisableAllCameras();
                    location.Camera.enabled = true;
                    _currentLocation = locationName;

                    _audioSource.clip = _tabletSounds.ChangeCamera;
                    _audioSource.Play();
                }
                else
                {
                    Debug.LogError($"{nameof(_locationsContainer)} does not contain a {nameof(location)} " +
                        $"named {locationName} in {nameof(locationName)} param!");
                }
            }
        }

        protected override void OnHidden()
        {
            _mainCamera.farClipPlane = _cameraStartFarClipPlane;
            _tabletCanvas.enabled = false;

            DisableAllCameras();

            _audioSource.clip = _tabletSounds.Hide;
            _audioSource.Play();
        }
        protected override void OnShown()
        {
            _mainCamera.farClipPlane = _mainCamera.nearClipPlane + 0.1f;
            _tabletCanvas.enabled = true;

            Location currentLocation;
            if (_locationsContainer.TryFindByName(_currentLocation, out currentLocation))
            {
                DisableAllCameras();
                currentLocation.Camera.enabled = true;
                _audioSource.clip = _tabletSounds.Show;
                _audioSource.Play();
            }
            else
            {
                Debug.LogError($"No {nameof(Location)}s found with the name {_currentLocation}");
            }
        }

        private void DisableAllCameras()
        {
            foreach (Location i in _locationsContainer)
            {
                i.Camera.enabled = false;
            }
        }
    }
}
