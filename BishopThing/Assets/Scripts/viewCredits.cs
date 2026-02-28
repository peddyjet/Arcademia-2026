using UnityEngine;
using UnityEngine.SceneManagement;

public class goToCredits : MonoBehaviour
{
    public void goCredits()
    {
        // if this ever runs then just know that some poor soul wanted to know who made this train wreck...
        SceneManager.LoadScene(6);
    }
}