using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace FiveNightsAtSnusoed
{
    [Serializable]
    public class Location
    {
        [SerializeField] private string _name = "defaultLocation";
        [SerializeField] private Camera _camera = null;
        
        private List<Entity> _enemys = new List<Entity>();

        private ReadOnlyCollection<Entity> _readOnlyEnemysCollection;

        public string Name { get => _name; }
        public Camera Camera { get => _camera; }
        public ReadOnlyCollection<Entity> Enemies { get => _readOnlyEnemysCollection; }
        
        public Location()
        {
            _readOnlyEnemysCollection = _enemys.AsReadOnly();
        }

        public void ComeIn(Entity entity)
        {
            if (_enemys.Any((e) => e.GetType() == entity.GetType()))
            {
                throw new ArgumentException($"Сreature with type {entity.GetType()} is already in the {nameof(Location)}!" +
                    $"The {nameof(Location)} cannot contain 2 creatures with the same type.");
            }
            else 
            {
                _enemys.Add(entity);
            }
        }
        public void GoOut(Entity entity)
        {
            if (_enemys.Any((e) => e.GetType() == entity.GetType()))
            {
                _enemys.Remove(entity);
            }
            else 
            {
                throw new ArgumentException($"Creatures with type {entity.GetType()} are not in the {nameof(Location)}!" +
                        $"You cannot leave the {nameof(Location)} without being in it.");
            }
        }
    }
}
