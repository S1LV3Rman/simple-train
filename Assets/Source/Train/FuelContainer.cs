using System;
using UnityEngine;

namespace Source
{
    public class FuelContainer : TrackableValue
    {
        [SerializeField] private float _maxFuel;
        [SerializeField] private float _currentFuel;

        public override float GetValue() => _currentFuel / _maxFuel;
        
        public float RequestFuel(float requestedAmount)
        {
            if (requestedAmount < 0f)
                throw new ArgumentOutOfRangeException(nameof(requestedAmount), requestedAmount,
                    "Requested amount of fuel can't be les then 0");
            
            var fuelToGive = Mathf.Min(requestedAmount, _currentFuel);
            _currentFuel -= fuelToGive;
            return fuelToGive;
        }

        public void AddFuel(float amountToAdd)
        {
            _currentFuel += Mathf.Min(amountToAdd, _maxFuel - _currentFuel);
        }
    }
}