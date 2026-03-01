using UnityEngine;

class InvisibilityPotion : MonoBehaviour, ICollectible
{
    public string Message => "+ Invisibility Potion";
    [SerializeField] private float _duration;

    public void Collect()
    {
        float duration = _duration;
        FindFirstObjectByType<PandorasBox>().IssuePotion(new Potion { ConsumeMessage = "- Invisibility Potion", OnConsumption = player => StartCoroutine(player.GiveIFrames(duration)), TargetUUID = "3" });
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    { 
        if (collision.gameObject.TryGetComponent<PandorasBox>(out var p))
            p.AddCollectible(this);
    }
}