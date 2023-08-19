using UnityEngine;

namespace Source
{
    public class FuelContainer : MonoBehaviour
    {
        [SerializeField] private float _fuelAmount;

        public float GetFuelAmount() => _fuelAmount;
        
        public float RequestFuel(float requestedAmount)
        {
            var fuelToGive = Mathf.Min(requestedAmount, _fuelAmount);
            _fuelAmount -= fuelToGive;
            return fuelToGive;
        }

        public void AddFuel(float amountToAdd)
        {
            _fuelAmount += amountToAdd;
        }
    }
}