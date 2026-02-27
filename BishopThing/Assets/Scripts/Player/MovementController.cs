using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _accelerationSpeed = 5f;
    [SerializeField] private Rigidbody2D _rb;

    private Vector2 _directionOfTravel;

    public void OnPlayerMove(InputAction.CallbackContext input)
    {
        _directionOfTravel = input.ReadValue<Vector2>();
    }

    private void Update()
    {
        HandleVelocity();
    }

    private void HandleVelocity()
    {
        if (_directionOfTravel == Vector2.zero)
        {
            _rb.linearVelocity = Vector2.zero;
        }
        else
        {
            _rb.linearVelocity = Vector2.Lerp(_rb.linearVelocity, _directionOfTravel * _speed, _accelerationSpeed * Time.deltaTime);
        }
    }
}
