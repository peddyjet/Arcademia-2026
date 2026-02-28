using UnityEngine;
using UnityEngine.SceneManagement;

public class skipCutscene : MonoBehaviour
{
    public void skipCutsceneOne()
    {
        // skip the first cutscene and go str8 to the game. Game index = 1
        SceneManager.LoadScene(1);
    }
}