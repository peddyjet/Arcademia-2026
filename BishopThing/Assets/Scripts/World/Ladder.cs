using UnityEngine;
using UnityEngine.Events;

public class Ladder : MonoBehaviour
{
    [SerializeField] private Ladder _twin;
    [SerializeField] private Vector2 _offset;
    [SerializeField] private UnityEvent _onFirstAccess;

    private bool _firstAccess = true;

    public Vector3 Position => transform.position + new Vector3(_offset.x, _offset.y, 0);
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<PlayerController>(out var pc))
        {
            if (_firstAccess) _onFirstAccess.Invoke(); 
            _firstAccess = false;
            pc.Teleport(_twin.Position);
        }
    }
}
