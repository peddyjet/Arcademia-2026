using UnityEngine;

public class Blessing : MonoBehaviour, ICollectible
{
    public string Message => "+ Blessing";

    public void Collect()
    {
        FindFirstObjectByType<PlayerController>().CurrentBlessings++;
        Destroy(gameObject);
    }
}
