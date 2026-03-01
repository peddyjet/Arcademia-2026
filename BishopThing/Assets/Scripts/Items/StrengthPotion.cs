using UnityEngine;

class StrengthPotion : MonoBehaviour, ICollectible
{
    public string Message => "+ Strength Potion";
    [SerializeField] private float _additionalPower;
    [SerializeField] private float _duration;

    public void Collect()
    {
        var additionalPower = _additionalPower;
        var duration = _duration;

        FindFirstObjectByType<PandorasBox>().IssuePotion(new Potion { ConsumeMessage = "- Strength Potion", OnConsumption = player => player.Buff(additionalPower, duration), TargetUUID = "2" });
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PandorasBox>(out var p))
            p.AddCollectible(this);
    }
}