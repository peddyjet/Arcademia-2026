using UnityEngine;

class StrengthPotion : MonoBehaviour, ICollectible
{
    public string Message => "+ Strength Potion";
    [SerializeField] private float _additionalPower;
    [SerializeField] private float _duration;

    public void Collect()
    {
        FindFirstObjectByType<PandorasBox>().IssuePotion(new Potion { ConsumeMessage = "- Strength Potion", OnConsumption = player => player.Buff(_additionalPower, _duration), TargetUUID = "2" });
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PandorasBox>(out var p))
            p.AddCollectible(this);
    }
}