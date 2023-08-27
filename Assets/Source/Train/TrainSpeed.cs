using UnityEngine;

namespace Source
{
    public class TrainSpeed : TrackableValue
    {
        private const float TO_KM_PER_H = 3.6f;
        
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _minSpeed;

        private float _speed;

        private void Update()
        {
            if (_rigidbody.velocity.magnitude < _minSpeed)
                _rigidbody.velocity = Vector2.zero;
            
            var direction = Vector2.Dot(transform.up, _rigidbody.velocity);
            _speed = _rigidbody.velocity.magnitude * Mathf.Sign(direction) * TO_KM_PER_H;
        }

        public override float GetValue() => _speed;
    }
}