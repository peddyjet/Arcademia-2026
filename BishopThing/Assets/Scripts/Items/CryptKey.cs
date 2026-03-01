using UnityEngine;

public class CryptKey : MonoBehaviour, ICollectible
{
    public string Message => "+ Crypt Key";

    public void Collect()
    {
        FindFirstObjectByType<PandorasBox>().CryptKeys++;
        Destroy(gameObject);
    }
}
