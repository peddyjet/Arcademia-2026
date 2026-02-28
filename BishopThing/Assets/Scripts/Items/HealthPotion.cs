using UnityEngine;

class HealthPotion : MonoBehaviour, ICollectible
{
    public string Message => "+ Health Potion";
    [SerializeField] private float _heal = 40;

    public void Collect()
    {
        FindFirstObjectByType<PandorasBox>().IssuePotion(new Potion { ConsumeMessage = "- Health Potion", OnConsumption = player => player.Heal(_heal), TargetUUID = "1" });
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PandorasBox>(out var p))
            p.AddCollectible(this);
    }
}