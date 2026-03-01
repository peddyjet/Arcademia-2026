using UnityEngine;

public class KeyGate : MonoBehaviour, ICollectible
{
    public string Message => "You try unlock the door (needs a key)";

    public void Collect()
    {
        var b = FindFirstObjectByType<PandorasBox>();
        if (b.Keys < 1) return;

        b.Keys--;
        Destroy(gameObject);
    }
}
