using UnityEngine;

public class BookTrap : MonoBehaviour, ICollectible
{
    [SerializeField] private int _extremity = 1;

    public void Collect()
    {
        FindFirstObjectByType<PandorasBox>().OpenBox(_extremity);
        Destroy(gameObject);
    }
}
