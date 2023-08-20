using UnityEngine;

namespace Source
{
    public class ValueTracker : MonoBehaviour
    {
        [SerializeField] private TrackableValue _value;
        [SerializeField] private ValueDisplay _display;

        private void Update()
        {
            _display.SetValue(_value.GetValue());
        }
    }
}