using Source;
using UnityEngine;

public class LocomotiveEngine : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private FuelContainer _fuelContainer;
    
    [SerializeField] private float _fuelPerSecond;
    [SerializeField] private float _force;
    
    void Update()
    {
        var direction = Input.GetAxis("Vertical");
        if (!Mathf.Approximately(direction, 0f))
        {
            var requiredFuel = Time.deltaTime * _fuelPerSecond;
            var fuel = _fuelContainer.RequestFuel(requiredFuel * Mathf.Abs(direction));
            var forceMultiplier = _force * fuel / requiredFuel;
            var force = Vector2.up * forceMultiplier * Mathf.Sign(direction);
            _rigidbody.AddForce(force);
        }
    }
}
