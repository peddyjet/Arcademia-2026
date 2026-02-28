using Assets.Scripts.Enemies;
using std = System;
using TMPro;
using UnityEngine;

public class Rat : Enemy
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _accelerationSpeed = 5f;
    [SerializeField] private float _damage = 5f;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _maxHealth = 20f;
    [SerializeField] [Min(0)] private float _minVariance = 1.0f;
    [SerializeField] [Min(0)] private float _maxVarience = 1.0f;
    [SerializeField] private float _damageIntensity = 1f;
    [SerializeField][Min(0)] private float _timeBeforeDirectionChange = 2f;

    private float _currentHealth;
    private float _currentVariance;
    private SpriteRenderer _spriteRenderer;
    private Color _originColor;

    private Transform _playerTransform;

    private std::DateTime _lastUpdate = std::DateTime.Now;
    private Vector2 _direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        _playerTransform = FindFirstObjectByType<PandorasBox>().transform;
        _currentHealth = _maxHealth;
        _currentVariance = Random.Range(-_minVariance, _maxVarience);
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originColor = _spriteRenderer.color;
    }

    private void FixedUpdate()
    {
        if (std::DateTime.Now > _lastUpdate.AddSeconds(_timeBeforeDirectionChange))
        {
            _direction = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));

            if (_direction == Vector2.zero) _direction = Vector2.one;
        }

        Vector2 targetVelocity = _direction * (_speed + _currentVariance);

        _rb.linearVelocity = Vector2.Lerp(
            _rb.linearVelocity,
            targetVelocity,
            _accelerationSpeed * Time.fixedDeltaTime
        );
    }



    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && tag == "Enemy")
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
                var normalised = Mathf.Clamp(_currentHealth / (_maxHealth + _damageIntensity), 0, 1);
                _spriteRenderer.color = _originColor * new Color(normalised, normalised, normalised);
                StartCoroutine(GracePeriod());
            }
        }
    }

}
