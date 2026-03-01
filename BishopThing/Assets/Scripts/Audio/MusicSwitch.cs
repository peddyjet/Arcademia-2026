// Change the music track once the player completes the first couple stages

using UnityEngine;

public class MusicSwitch : MonoBehaviour
{
    public int trackNumber;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindFirstObjectByType<GameAudioManager>().StartNewLevel(trackNumber);
        }
    }
}