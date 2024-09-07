using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainMenuController : MonoBehaviour
{
    public Button playButton;
    public Button quitButton;
    public GameObject levelSelection;

    void Awake()
    {
        playButton.onClick.AddListener(PlayGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    void PlayGame()
    {
        levelSelection.SetActive(true);
    }

    void QuitGame()
    {
        Application.Quit();
    }

}
