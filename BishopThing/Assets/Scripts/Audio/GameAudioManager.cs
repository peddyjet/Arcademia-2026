// Untested, unimplemented

using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;

public class GameAudioManager : MonoBehaviour
{
    public AudioSource music;
    public AudioSource levelStart;

    public AudioClip loadLevel;
    public AudioClip[] levelTunes;

    public void StartNewLevel(int levelIndex){
        StartCoroutine(PlayLevelSequence(levelIndex));
    }

    IEnumerator PlayLevelSequence(int levelIndex){
        // stop current track
        music.Stop();

        // new round? happy days - play the initial tune
        levelStart.PlayOneShot(loadLevel);

        // wait a bit for the initial tune to run its course
        yield return new WaitForSeconds(2f);

        // play the level music
        music.clip = levelTunes[levelIndex];
        music.Play();
    }
}