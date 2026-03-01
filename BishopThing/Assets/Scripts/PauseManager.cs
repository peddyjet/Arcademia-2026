using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject firstButton;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.activeSelf)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0){
            if (EventSystem.current.currentSelectedGameObject != null){
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f; //no more bishops box for the time being

        EventSystem.current.SetSelectedGameObject(firstButton);
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f; //a lot more bishops box for the time being (until the player dies. Games hard asf)
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0); //back to the main menu
    }

    public void ExitGame()
    {
        Application.Quit(); //bye byeeeeee
    }
}