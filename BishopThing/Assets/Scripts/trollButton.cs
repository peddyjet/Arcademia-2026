using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TrollGameOverButton : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void trollText()
    {
        // lol
        text.text = "NO CHANCE!\nSkill Issue";
    }

    public void playAgain()
    {
        SceneManager.LoadScene(3);
    }
}