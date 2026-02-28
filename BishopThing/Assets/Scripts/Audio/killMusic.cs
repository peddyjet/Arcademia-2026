using System.Runtime.CompilerServices;
using UnityEngine;

public class killMusic : MonoBehaviour
{
    public void stopMainMenuMusic()
    {
        // I hate this
        MenuAudioManager.instance.StopMenuMusic();
    }
}