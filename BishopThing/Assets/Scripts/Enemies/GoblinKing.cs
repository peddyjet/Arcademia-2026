using Assets.Scripts.Enemies;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoblinKing : Enemy
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _accelerationSpeed = 5f;
    [SerializeField] private float _damage = 5f;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _maxHealth = 20f;
    [SerializeField][Min(0)] private float _minVariance = 1.0f;
    [SerializeField][Min(0)] private float _maxVarience = 1.0f;
    [SerializeField] private float _damageIntensity = 1f;
    [SerializeField] private float _wanderRadius = 5f;
    [SerializeField] private float _changeDirectionTime = 2f;
    [SerializeField] private float _avoidDistance = 3f;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _shootCooldown = 2f;
    [SerializeField] private float _shootRange = 8f;

    private float _shootTimer;
    private Vector2 _currentDirection;
    private float _directionTimer;
    private float _currentHealth;
    private float _currentVariance;
    private SpriteRenderer _spriteRenderer;
    private Color _originColor;

    private Transform _playerTransform;

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
        if (_playerTransform == null)
        {
            _playerTransform = FindFirstObjectByType<PandorasBox>()?.transform;
            return;
        }

        

        _shootTimer -= Time.deltaTime;

        float distanceToPlayer = Vector2.Distance(transform.position, _playerTransform.position);

        if (distanceToPlayer <= _shootRange && _shootTimer <= 0f)
        {
            ShootAtPlayer();
            _shootTimer = _shootCooldown;
        }

        Vector2 moveDirection;

            // Wander randomly
            _directionTimer -= Time.fixedDeltaTime;

            if (_directionTimer <= 0f)
            {
                _currentDirection = Random.insideUnitCircle.normalized;
                _directionTimer = _changeDirectionTime;
            }

            moveDirection = _currentDirection;

        Vector2 targetVelocity = moveDirection * (_speed + _currentVariance);

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
        if (collision.gameObject.tag == "PlayerWeapon")
        {
            _currentHealth -= _playerTransform.GetComponent<PlayerController>().Damage;
            if (_currentHealth <= 0)
            {
                Destroy(gameObject);
                SceneManager.LoadScene(7);
            }
            else
            {
                var normalised = Mathf.Clamp(_currentHealth / (_maxHealth + _damageIntensity), 0, 1);
                _spriteRenderer.color = _originColor * new Color(normalised, normalised, normalised);
                StartCoroutine(GracePeriod());
            }
        }
    }

    private void ShootAtPlayer()
    {
        Vector2 direction = (_playerTransform.position - _shootPoint.position).normalized;

        GameObject projectile = Instantiate(
            _projectilePrefab,
            _shootPoint.position,
            Quaternion.identity
        );

        projectile.GetComponent<GoblinProjectile>().SetDirection(direction);
    }

}
