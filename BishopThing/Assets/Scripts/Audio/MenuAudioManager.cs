using UnityEngine;

// For the main menu music - it'll ensure that it doesnt overlap the same track
public class MenuAudioManager : MonoBehaviour
{
    public static MenuAudioManager instance;

    void Start(){
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // kill the tunes once we start the game
    public void StopMenuMusic()
    {
        Destroy(gameObject);
    }
}