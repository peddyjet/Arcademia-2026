using UnityEngine;

public class BookTrap : MonoBehaviour, ICollectible
{
    [SerializeField] private int _extremity = 1;

    public string Message => "+ Irrelevant Book";

    public void Collect()
    {
        FindFirstObjectByType<PandorasBox>().OpenBox(_extremity);
        Destroy(gameObject);
    }
}
