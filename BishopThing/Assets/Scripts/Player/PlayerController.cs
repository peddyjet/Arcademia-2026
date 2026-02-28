using std = System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;

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
    private float _defaultDamage;

    // audio stuff
    public AudioSource bishopSource;
    public AudioClip attackClip;
    public AudioClip damageClip;
    public AudioClip blessingsClip;

    private void Start()
    {
        _defaultDamage = _damageAmount;
        _currentHealth = _maxHealth;
        CurrentBlessings = _startingBlessings;
        OnBlessingUsed += () =>
        {
            // audio stuff
            bishopSource.Stop();
            bishopSource.PlayOneShot(blessingsClip);

            CurrentBlessings--;
            _currentHealth = Random.Range(_maxHealth * 0.5f, _maxHealth);
            GetComponent<PandorasBox>().OpenBox(Random.Range(11, 20) / 10);
            StartCoroutine(GiveIFrames(_iframes));
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

        bishopSource.Stop();
        // why not?
        bishopSource.pitch = Random.Range(0.85f,1.15f);
        bishopSource.PlayOneShot(damageClip);

        if (_currentHealth <= 0)
        {
            if(CurrentBlessings > 0)
            {
                OnBlessingUsed?.Invoke();
            }
            else SceneLoader.LoadSceneIdxStatic(5);
            return;
        }
        StartCoroutine(GiveIFrames(_iframes));
    }

    protected IEnumerator<WaitForSeconds> GiveIFrames(float frames)
    {
        var sprite = _sprite;
        tag = "Untagged";
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.5f);
        yield return new WaitForSeconds(frames / 3);
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1f);
        yield return new WaitForSeconds(frames / 3);
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.5f);
        yield return new WaitForSeconds(frames / 3);
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1f);
        tag = "Player";
    }

    public void OnAttack(InputAction.CallbackContext input)
    {
        if(_lastAttackTime + std::TimeSpan.FromSeconds(_attackCooldown) > std::DateTime.Now) return;

        _lastAttackTime = std::DateTime.Now;
        _meleeAttackSlash.SetTrigger("Attack");
        _meleeAttackGyrator.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(_directionOfTravel.y, _directionOfTravel.x) * Mathf.Rad2Deg);

        bishopSource.Stop();

        // because again, why not?
        bishopSource.pitch = Random.Range(0.9f,1.1f);

        bishopSource.PlayOneShot(attackClip);
    }

    public void UseHealPotionHandler(InputAction.CallbackContext context)
    {
        if (context.performed) GetComponent<PandorasBox>().UsePotion("1");
    }

    public void UseStrengthPotionHandler(InputAction.CallbackContext context)
    {
        if (context.performed) GetComponent<PandorasBox>().UsePotion("2");
    }

    public void UseInvisibilityPotionHandler(InputAction.CallbackContext context)
    {
        if (context.performed) GetComponent<PandorasBox>().UsePotion("3");
    }

    public void Bless()
    {
        if (CurrentBlessings > 0)
        {
            OnBlessingUsed?.Invoke();
        }
    }

    public void Teleport(Vector3 position) => transform.position = position;
    public void Heal(float amount) => _currentHealth = Mathf.Min(_currentHealth + amount, _maxHealth);

    public void Buff(float amount, float duration)
    {
        IEnumerator Coroutine()
        {
            _damageAmount = amount;
            yield return new WaitForSeconds(duration);
            _damageAmount = _defaultDamage;
        }

        StartCoroutine(Coroutine());
    }
}
