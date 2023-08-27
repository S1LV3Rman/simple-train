using System;
using Source;
using UnityEngine;
using UnityEngine.InputSystem;

public class LocomotiveEngine : MonoBehaviour
{
    private enum EngineState
    {
        Idle,
        Moving,
        Stopping
    }
    
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private FuelContainer _fuelContainer;
    [SerializeField] private TrainSpeed _speed;
    
    [SerializeField] private float _fuelPerSecond;
    [SerializeField] private float _force;
    [SerializeField] private float _defaultDrag;
    [SerializeField] private float _stoppingDrag;
    
    private ControlsConfig _controls;
    private InputAction _moveAction;

    private EngineState _state;
    private bool _stopPressed;

    private EngineState State
    {
        get => _state;
        set
        {
            if (_state == value)
                return;

            _state = value;
            switch (value)
            {
                case EngineState.Idle:
                case EngineState.Moving:
                    _rigidbody.drag = _defaultDrag;
                    break;
                case EngineState.Stopping:
                    _rigidbody.drag = StoppingDrag;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }
    }

    private void UpdateState()
    {
        if (_stopPressed)
        {
            State = EngineState.Stopping;
            return;
        }

        var direction = _moveAction.ReadValue<float>();
        if (Mathf.Approximately(direction, 0f))
        {
            State = EngineState.Idle;
            return;
        }

        var speed = _speed.GetValue();
        if (Mathf.Approximately(speed, 0f))
        {
            State = EngineState.Moving;
            return;
        }

        State = speed * direction < 0f 
            ? EngineState.Stopping
            : EngineState.Moving;
    }

    private float StoppingDrag => Mathf.Lerp(
        _defaultDrag, _stoppingDrag,
        1f / (1 + Mathf.Abs(_speed.GetValue())));

    private void Awake()
    {
        _controls = new ControlsConfig();
        _moveAction = _controls.Gameplay.Move;
        _controls.Gameplay.Stop.performed += ToggleStopping;

        State = EngineState.Idle;
    }

    private void ToggleStopping(InputAction.CallbackContext obj) => _stopPressed = !_stopPressed;

    void Update()
    {
        UpdateState();
        switch (State)
        {
            case EngineState.Idle:
                break;
            case EngineState.Moving:
                var direction = _moveAction.ReadValue<float>();
                Move(direction);
                break;
            case EngineState.Stopping:
                _rigidbody.drag = StoppingDrag;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Move(float direction)
    {
        var requiredFuel = Time.deltaTime * _fuelPerSecond;
        var fuel = _fuelContainer.RequestFuel(requiredFuel * Mathf.Abs(direction));
        var forceMultiplier = _force * fuel / requiredFuel;
        var force = (Vector2) transform.up * forceMultiplier * Mathf.Sign(direction);

        _rigidbody.AddForce(force);

        State = EngineState.Moving;
    }

    void OnEnable()
    {
        _controls.Gameplay.Enable();
    }
    void OnDisable()
    {
        _controls.Gameplay.Disable();
    }
}
