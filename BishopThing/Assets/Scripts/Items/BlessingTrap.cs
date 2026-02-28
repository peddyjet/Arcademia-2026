using UnityEngine;

public class BlessingTrap : MonoBehaviour, ICollectible
{
    [SerializeField] private int _extremity = 1;

    public string Message => "+ Fake Blessing";

    public void Collect()
    {
        FindFirstObjectByType<PandorasBox>().OpenBox(_extremity);
        Destroy(gameObject);
    }
}
