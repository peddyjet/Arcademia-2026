using UnityEngine;

class InvisibilityPotion : MonoBehaviour, ICollectible
{
    public string Message => "+ Invisibility Potion";
    [SerializeField] private float _duration;

    public void Collect()
    {
        FindFirstObjectByType<PandorasBox>().IssuePotion(new Potion { ConsumeMessage = "- Invisibility Potion", OnConsumption = player => StartCoroutine(player.GiveIFrames(_duration)), TargetUUID = "3" });
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PandorasBox>(out var p))
            p.AddCollectible(this);
    }
}