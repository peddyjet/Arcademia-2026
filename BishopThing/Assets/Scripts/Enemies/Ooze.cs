using Assets.Scripts.Enemies;
using UnityEngine;

public class Ooze : Enemy
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _accelerationSpeed = 5f;
    [SerializeField] private float _damage = 5f;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _maxHealth = 20f;
    [SerializeField] [Min(0)] private float _minVariance = 1.0f;
    [SerializeField] [Min(0)] private float _maxVarience = 1.0f;

    private float _currentHealth;
    private float _currentVariance;

    private Transform _playerTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        _playerTransform = FindFirstObjectByType<PandorasBox>().transform;
        _currentHealth = _maxHealth;
        _currentVariance = Random.Range(-_minVariance, _maxVarience);
    }

    private void FixedUpdate()
    {
        if (_playerTransform != null)
        {
            Vector2 directionToPlayer =
                (_playerTransform.position - transform.position).normalized;

            Vector2 targetVelocity = directionToPlayer * _speed;

            _rb.linearVelocity = Vector2.Lerp(
                _rb.linearVelocity,
                targetVelocity,
                (_accelerationSpeed + _currentVariance) * Time.fixedDeltaTime
            );
        }
    }



    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _playerTransform.GetComponent<PlayerController>().TakeDamage(_damage);
            StartCoroutine(GracePeriod());
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _currentHealth -= _playerTransform.GetComponent<PlayerController>().Damage;
            if (_currentHealth <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                StartCoroutine(GracePeriod());
            }
        }
    }

}
