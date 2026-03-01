using UnityEngine;

public class CryptGate : MonoBehaviour, ICollectible
{
    public string Message => "You try unlock the door (needs two crypt keys)";

    public void Collect()
    {
        var b = FindFirstObjectByType<PandorasBox>();
        if (b.CryptKeys < 2) return;

        b.CryptKeys -= 2;
        Destroy(gameObject);
    }
}
