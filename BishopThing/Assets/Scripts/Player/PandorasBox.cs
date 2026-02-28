using Assets.Scripts.Enemies;
using System.Collections.Generic;
using System;
using UnityEngine;

/// <summary>
/// Handles the logic of collecting items and opening Pandora's Box. The box will open every time a new item is collected, resulting in a swarm of enemies.
/// </summary>
public class PandorasBox : MonoBehaviour
{

    private ICollectible[] collectibles;

    public ICollectible[] Collectibles => collectibles;
    [Header("Pandora's Box Settings")]
    [SerializeField] private PandoraWeights _pandoraWeights;
    [SerializeField] private Animator _animator;
    [SerializeField] private PandorasCanvas _pandoraCanvas;

    [Header("Hyperparameters")]
    [SerializeField][Min(0)] private int _lowerBoundSpawn = 1;
    [SerializeField][Min(1)] private int _upperBoundSpawn = 5;
    [SerializeField][Min(0)] private float _lowerBoundVelocity = 1;
    [SerializeField][Min(0)] private float _upperBoundVelocity = 5;
    [SerializeField][Range(0f,2f)] private float _offset = 0.3f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out ICollectible collectible))
        {
            AddCollectible(collectible);
        }
    }

    public void AddCollectible(ICollectible collectible)
    {
        // Declare to collectible that it has been collected, so it can do its own logic (e.g. disappear, play sound, etc.)
        collectible.Collect();
         
        // Add collectible to inventory
        if (collectibles != null)
        {
            var structure = new ICollectible[collectibles.Length + 1];
            Array.Copy(collectibles, structure, collectibles.Length);
            structure[structure.Length - 1] = collectible;
            return;
        }

        collectibles = new ICollectible[] { collectible };

        // TODO: Add Logic to open Pandora's Box every time a collectible is added.
        OpenBox(message: collectible.Message);
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
            var enemies = _pandoraWeights.GetRandomEnemies(numberToSpawn, PandoraWeights.EnemyType.None);
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

    void Start()
    {
        StartCoroutine(GracePeriod());
    }

    IEnumerator<WaitForSeconds> GracePeriod()
    {
        yield return new WaitForSeconds(3);
        
        OpenBox(message: "Go fuck yourself");
    }
}
