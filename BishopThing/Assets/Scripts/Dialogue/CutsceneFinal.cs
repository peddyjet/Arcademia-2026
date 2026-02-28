using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;

public class CutsceneFinal : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    private List<DialogueLine> lines = new List<DialogueLine>();
    private int currentLineIndex = 0;

    void Start()
    {
        lines.Clear();
        lines.Add(new DialogueLine { speaker = "Bishop", text = "*Out of breath panting*" });
        lines.Add(new DialogueLine { speaker = "Goblin", text = "Well well well..." });
        lines.Add(new DialogueLine { speaker = "Bishop", text = "We meet again at last!" });
        lines.Add(new DialogueLine { speaker = "Goblin", text = "Save your breath fool, you're about to face my wrath!" });
        lines.Add(new DialogueLine { speaker = "Bishop", text = "What kind of wrath is that? You found it in a cereal box?" });
        lines.Add(new DialogueLine { speaker = "Goblin", text = "Shush, I have excellent throwing skills" });
        lines.Add(new DialogueLine { speaker = "Bishop", text = "... So you're going to throw cereal at me?" });
        lines.Add(new DialogueLine { speaker = "Goblin", text = "NO! I'm going to throw keys at you to open the box" });
        lines.Add(new DialogueLine { speaker = "Goblin", text = "P R E P A R E   T O   D I E" });

        currentLineIndex = 0;
    }

    public void ShowNextLine()
    {
        if (currentLineIndex < lines.Count)
        {
            textDisplay.text = lines[currentLineIndex].text;
            currentLineIndex++;
        }
        else
        {
            textDisplay.text = "";
            FinishCutscene();
        }
    }

    public void FinishCutscene()
    {
        //TODO - change this or figure out how to get to the final arena
        SceneManager.LoadScene(0);
    }
}