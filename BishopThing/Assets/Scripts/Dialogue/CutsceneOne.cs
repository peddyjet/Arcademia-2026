using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;

[System.Serializable]
public class DialogueLine
{
    public string speaker;
    public string text;
}

class CutsceneOne : MonoBehaviour
{
    public TextMeshProUGUI text;
    public void nextDialogue(string dialogue)
    {
        text.text = dialogue;
    }

    protected List<DialogueLine> lines = new List<DialogueLine>();

    private int currentLineIndex = 0;

    void Start(){

        lines.Clear();
        lines.Add(new DialogueLine { speaker = "Bishop", text = "HEY! Stop that, shoo! Go elsewhere." });
        lines.Add(new DialogueLine { speaker = "Teenagers", text = "Screw you old man!" });
        lines.Add(new DialogueLine { speaker = "Bishop", text = "Go now before the I phone the police!" });
        lines.Add(new DialogueLine { speaker = "Notice", text = "*The trouble makers leave the premises...*"});
        lines.Add(new DialogueLine { speaker = "Bishop", text = "Phew!" });
        lines.Add(new DialogueLine { speaker = "Bishop", text = "Hmmm, something smells off" });
        lines.Add(new DialogueLine { speaker = "Bishop", text = "What in the Jimothy Jones was that?" });
        lines.Add(new DialogueLine { speaker = "Goblin", text = "HAHAHAHAHA, eat my curse!" });
        lines.Add(new DialogueLine { speaker = "Bishop", text = "What on Earth is this mysterious box?" });
        lines.Add(new DialogueLine { speaker = "Goblin", text = "It's Pandoras Box! You're cursed, fool!" });
        lines.Add(new DialogueLine { speaker = "Goblin", text = "You must collect eight keys in order to open the box and be free from the curse!" });
        lines.Add(new DialogueLine { speaker = "Goblin", text = "You can attack enemies with SPACEBAR, move with WASD, I assume you've played a game before?" });
        lines.Add(new DialogueLine { speaker = "Goblin", text = "E to interact with things.. blah blah. if you don't know this drill go start with Minecraft!" });
        lines.Add(new DialogueLine { speaker = "Goblin", text = "Good luck, you'll need it!"});
        lines.Add(new DialogueLine { speaker = "Bishop", text = "EURGH! My dinner will get cold!"});

        currentLineIndex = 0;
    }

    public void showText()
    {
        if (currentLineIndex < lines.Count)
        {
            nextDialogue(lines[currentLineIndex].text);
            currentLineIndex++;
        }
        else
        {
            text.text = "";
            GoToLevelOne();
            // if it breaks here then. Oops lol there's a glitch in the matrix
        }
    }

    public void GoToLevelOne()
    {
        // TODO: CHANGE THIS SHIZ! 4 = Playground
        SceneManager.LoadScene(4);
    }
}