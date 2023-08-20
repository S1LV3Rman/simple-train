using Lean.Gui;
using UnityEngine;

namespace Source
{
    public class AddFuelButton : MonoBehaviour
    {
        [SerializeField] private LeanButton _button;
        [SerializeField] private FuelContainer _fuelContainer;

        [SerializeField] private float _amountToAdd;

        private void Awake()
        {
            _button.OnClick.AddListener(AddFuel);
        }

        private void AddFuel()
        {
            _fuelContainer.AddFuel(_amountToAdd);
        }
    }
}