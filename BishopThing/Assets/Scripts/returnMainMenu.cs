using UnityEngine;
using UnityEngine.SceneManagement;

public class returnMainMenu : MonoBehaviour
{
    public void goMainMenu()
    {
        // do the deed... runs if someones dead (oops. Most likely a skill issue on their end)
        SceneManager.LoadScene(0);
    }
}