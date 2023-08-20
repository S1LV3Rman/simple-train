using UnityEngine;

namespace Source
{
    public class TrainSpeed : TrackableValue
    {
        private const float TO_KM_PER_H = 3.6f;
        
        [SerializeField] private Rigidbody2D _rigidbody;

        public override float GetValue() => _rigidbody.velocity.magnitude * TO_KM_PER_H;
    }
}