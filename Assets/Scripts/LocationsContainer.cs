using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FiveNightsAtSnusoed
{
    public class LocationsContainer : MonoBehaviour, IEnumerable
    {       
        [SerializeField] private Location[] _locations = new Location[0];

        private Dictionary<string, Location> _locationsDictionary = new Dictionary<string, Location>();

        public bool ContainLocationWithName(string locationName)
        {
            TryFillDictionary();
            return _locationsDictionary.ContainsKey(locationName);        
        }
        public bool TryFindByName(string locationName, out Location location)
        {
            TryFillDictionary();
            return _locationsDictionary.TryGetValue(locationName, out location);              
        }              
        public IEnumerator GetEnumerator() => _locations.GetEnumerator();

        private void TryFillDictionary() 
        {
            if (_locations.Length != _locationsDictionary.Count)
            {
                _locationsDictionary.Clear();
                foreach (Location location in _locations)
                    _locationsDictionary.Add(location.Name, location);
            }
        }
    }

}