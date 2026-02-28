using UnityEngine;

public class HealthPotion : MonoBehaviour, IPotion
{
    private static float _heal = 30;

    public string Message => "+ Health Potion";

    public string ConsumeMessage => "- Health Potion";

    public string TargetUUID => "1";

    public void Collect()
    {
        gameObject.SetActive(false);
    }

    public void OnConsumption(PlayerController player)
    {
        player.Heal(_heal);
    }
}
