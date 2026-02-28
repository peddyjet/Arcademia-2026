using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;

public class GameAudioManager : MonoBehaviour
{
    public AudioSource music;
    public AudioSource levelStart;

    public AudioClip loadLevel;
    public AudioClip[] levelTunes;

    void Start()
    {
        StartNewLevel(0);
    }

    public void StartNewLevel(int levelIndex){
        StartCoroutine(PlayLevelSequence(levelIndex));
    }

    IEnumerator PlayLevelSequence(int levelIndex){
        // stop current track
        music.Stop();

        // new round? happy days - play the initial tune
        levelStart.PlayOneShot(loadLevel);

        // wait a bit for the initial tune to run its course (it's 8s)
        yield return new WaitForSeconds(8f);

        // play the level music
        music.clip = levelTunes[levelIndex];
        music.Play();
    }
}