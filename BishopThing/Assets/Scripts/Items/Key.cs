using UnityEngine;

public class Key : MonoBehaviour, ICollectible
{
    public string Message => "+ Key";

    public void Collect()
    {
        FindFirstObjectByType<PandorasBox>().Keys++;
        Destroy(gameObject);
    }
}
