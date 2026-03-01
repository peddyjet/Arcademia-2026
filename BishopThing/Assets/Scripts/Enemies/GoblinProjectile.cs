using UnityEngine;

public class GoblinProjectile : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _damage = 5f;
    [SerializeField] private float _lifeTime = 5f;

    private Vector2 _direction;

    public void SetDirection(Vector2 direction)
    {
        _direction = direction.normalized;
        Destroy(gameObject, _lifeTime);
    }

    private void Update()
    {
        transform.position += (Vector3)(_direction * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PandorasBox>().OpenBox(1, "Box Unlocked!");
            Destroy(gameObject);
        }
    }
}