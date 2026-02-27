using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public void LoadSceneIdx(int idx)     
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(idx);
    }

    public void LoadSceneName(string name)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
    }

    public static void LoadSceneIdxStatic(int idx)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(idx);
    }

    public static void LoadSceneNameStatic(string name)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
    }
}
