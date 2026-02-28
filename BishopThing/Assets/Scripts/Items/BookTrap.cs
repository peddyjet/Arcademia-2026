using UnityEngine;

public class BookTrap : MonoBehaviour, ICollectible
{
    [SerializeField] private int _extremity = 1;

    public string Message => "You picked up the book and put it into Pandora's Box.";

    public void Collect()
    {
        FindFirstObjectByType<PandorasBox>().OpenBox(_extremity);
        Destroy(gameObject);
    }
}
