using std = System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D), typeof(PandorasBox))]
public class PlayerController : MonoBehaviour
{
    [Header("Hyperparameters")]
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _accelerationSpeed = 5f;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private int _maxBlessings = 5;
    [SerializeField] private int _startingBlessings = 1;
    [SerializeField] private float _iframes = 1f;
    [SerializeField] private float _damageAmount = 15f;
    [SerializeField] private float _attackCooldown = 0.5f;
    [Header("UI Components")]
    [SerializeField] private Slider _healthBar;
    [SerializeField] private Slider _blessingBar;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Animator _meleeAttackSlash;
    [SerializeField] private GameObject _meleeAttackGyrator;

    public int CurrentBlessings { get; set; }
    private float _currentHealth;
    public event std::Action OnBlessingUsed;
    public float Damage => _damageAmount;

    private Vector2 _directionOfTravel;
    private std::DateTime _lastAttackTime = std::DateTime.Now;
    private bool _isMoving;

    private void Start()
    {
        _currentHealth = _maxHealth;
        CurrentBlessings = _startingBlessings;
        OnBlessingUsed += () =>
        {
            CurrentBlessings--;
            _currentHealth = Random.Range(_maxHealth * 0.5f, _maxHealth);
            GetComponent<PandorasBox>().OpenBox(Random.Range(11, 20) / 10);
            StartCoroutine(GiveIFrames());
        };
    }

    public void OnPlayerMove(InputAction.CallbackContext input)
    {
        if(input.performed)
            _directionOfTravel = input.ReadValue<Vector2>();
        
        if(input.performed != _isMoving)
        {
            GetComponent<Animator>().SetBool("isWalking", input.performed);
            _isMoving = input.performed;
        }
    }

    private void Update()
    {
        HandleVelocity();
        _healthBar.normalizedValue = _currentHealth / _maxHealth;
        _blessingBar.normalizedValue = (float)CurrentBlessings / _maxBlessings;
    }

    private void HandleVelocity()
    {
        if (_directionOfTravel == Vector2.zero || !_isMoving)
        {
            _rb.linearVelocity = Vector2.zero;
        }
        else
        {
            _rb.linearVelocity = Vector2.Lerp(_rb.linearVelocity, _directionOfTravel * _speed, _accelerationSpeed * Time.deltaTime);
            _sprite.transform.rotation = Quaternion.Euler(0, 0, (Mathf.Atan2(_directionOfTravel.y, _directionOfTravel.x) * Mathf.Rad2Deg)-90);
        }
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            if(CurrentBlessings > 0)
            {
                OnBlessingUsed?.Invoke();
            }
            else SceneLoader.LoadSceneIdxStatic(0);
            return;
        }
        StartCoroutine(GiveIFrames());
    }

    protected IEnumerator<WaitForSeconds> GiveIFrames()
    {
        var sprite = _sprite;
        tag = "Untagged";
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.5f);
        yield return new WaitForSeconds(_iframes / 3);
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1f);
        yield return new WaitForSeconds(_iframes / 3);
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.5f);
        yield return new WaitForSeconds(_iframes / 3);
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1f);
        tag = "Player";
    }

    public void OnAttack(InputAction.CallbackContext input)
    {
        if(_lastAttackTime + std::TimeSpan.FromSeconds(_attackCooldown) > std::DateTime.Now) return;

        _lastAttackTime = std::DateTime.Now;
        _meleeAttackSlash.SetTrigger("Attack");
        _meleeAttackGyrator.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(_directionOfTravel.y, _directionOfTravel.x) * Mathf.Rad2Deg);
    }

    public void Bless()
    {
        if (CurrentBlessings > 0)
        {
            OnBlessingUsed?.Invoke();
        }
    }

    public void Teleport(Vector3 position) => transform.position = position;
}
