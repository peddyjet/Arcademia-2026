using UnityEngine;

public class CryptGate : MonoBehaviour, ICollectible
{
    [SerializeField] GameObject GoblinKing;
    public string Message => "You scramble for two crypt keys...";

    public void Collect()
    {
        var b = FindFirstObjectByType<PandorasBox>();
        if (b.CryptKeys < 2) return;

        b.CryptKeys -= 2;
        GoblinKing.SetActive(true);
        Destroy(gameObject);
    }
}
