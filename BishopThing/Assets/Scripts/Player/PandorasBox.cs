using Assets.Scripts.Enemies;
using System.Collections.Generic;
using System;
using UnityEngine;
using Assets.Scripts.Items;
using System.Linq;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections;

/// <summary>
/// Handles the logic of collecting items and opening Pandora's Box. The box will open every time a new item is collected, resulting in a swarm of enemies.
/// </summary>
public class PandorasBox : MonoBehaviour
{

    private Dictionary<string, Stack<Potion>> potions = new();

    [Header("Pandora's Box Settings")]
    [SerializeField] private PandoraWeights _pandoraWeights;
    [SerializeField] private Animator _animator;
    [SerializeField] private PandorasCanvas _pandoraCanvas;
    [SerializeField] private TextMeshProUGUI _keyText;
    [SerializeField] private TextMeshProUGUI _cryptText;

    [Header("Hyperparameters")]
    [SerializeField][Min(0)] private int _lowerBoundSpawn = 1;
    [SerializeField][Min(1)] private int _upperBoundSpawn = 5;
    [SerializeField][Min(0)] private float _lowerBoundVelocity = 1;
    [SerializeField][Min(0)] private float _upperBoundVelocity = 5;
    [SerializeField][Range(0f,2f)] private float _offset = 0.3f;

    private int _permissableDifficulties = 28;
    public int Keys { get; set; }
    public int CryptKeys { get; set; }

    // audio
    public AudioSource bishopSource;
    public AudioClip collectable;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out ICollectible collectible))
        {
            AddCollectible(collectible);
        }
    }

    public void UsePotion(string uuid) 
    {
        if(!potions.ContainsKey(uuid) || potions[uuid].Count == 0)
        {
            OpenBox(1, "You couldn't find any of that potion!");
            return;
        }

        var potionList = potions[uuid];
        var potion = potionList.Pop();
        OpenBox(1, potion.ConsumeMessage);
        potion.OnConsumption(GetComponent<PlayerController>());
        UpdatePotionUI();
    }

    public void UpdatePotionUI()
    {
        var links = FindObjectsByType<UILink>(FindObjectsSortMode.None);
        foreach (var valuePair in potions)
        {
            links.First(i => i.UUID == valuePair.Key).GetComponent<TextMeshProUGUI>().text = valuePair.Value.Count.ToString();
        }
    }

    public void IssuePotion(Potion potion)
    {
        if (!potions.ContainsKey(potion.TargetUUID))
            potions.Add(potion.TargetUUID, new Stack<Potion>(new Potion[] {potion}));
        else potions[potion.TargetUUID].Push(potion);
        UpdatePotionUI();
    }

    public void AddCollectible(ICollectible collectible)
    {
        collectible.Collect();
        OpenBox(message: collectible.Message);
        bishopSource.PlayOneShot(collectable);
    }

    public void OpenBox(int iterations = 1, string message = "")
    {
        AnimateOpen();

        if (message != "")
            _pandoraCanvas.ShowMessage(message);

        for (int i = 0; i < iterations; i++)
        {
            var numberToSpawn = UnityEngine.Random.Range(_lowerBoundSpawn, _upperBoundSpawn + 1);
            var selectedEnemies = new List<Enemy>();
            var enemies = _pandoraWeights.GetRandomEnemies(numberToSpawn, _permissableDifficulties);
            foreach (var enemy in enemies)
            {
                var obj = Instantiate(enemy.gameObject, transform.position + new Vector3(UnityEngine.Random.Range(-_offset, _offset),
                    UnityEngine.Random.Range(-_offset, _offset), 0), Quaternion.identity);
                obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(UnityEngine.Random.Range(-_lowerBoundVelocity, _upperBoundVelocity),
                    UnityEngine.Random.Range(-_lowerBoundVelocity, _upperBoundVelocity)), ForceMode2D.Impulse);

            }
        }

        
    }

    private void AnimateOpen()
    {
        _animator.SetTrigger("Opening");
    }

    public void ChangePermit(int permit)
    {
        _permissableDifficulties = permit;
    }

    private void Update()
    {
        _keyText.text = Keys.ToString();
        _cryptText.text = CryptKeys.ToString() + "/2";
    }
}
