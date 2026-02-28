using UnityEngine;
using UnityEngine.SceneManagement;

public class returnMainMenu : MonoBehaviour
{
    public void goMainMenu()
    {
        // do the deed... runs if someones dead (oops. Probs a skill issue)
        SceneManager.LoadScene(0);
    }
}