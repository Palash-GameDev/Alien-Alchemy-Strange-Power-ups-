using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public Button pauseButton;
    public Button resumeButton;
    public Button HomeButton;
    public Button QuitButton;
    public TMP_Text coins;
    [SerializeField] private int coinCollected;
    

    public GameObject PausePanel;



    void Awake()
    {
        pauseButton.onClick.AddListener(PauseGame);
        resumeButton.onClick.AddListener(ResumeGame);
        HomeButton.onClick.AddListener(MoveToHome);
        QuitButton.onClick.AddListener(QuitGame);
    }
    void Start()
    {
        ShowCoins();
    }
void Update()
{
    if(Input.GetKeyDown(KeyCode.Escape))
    {
        PauseGame();
    }
}
    void PauseGame()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    void ResumeGame()
    {
        Time.timeScale = 1;
        PausePanel.SetActive(false);
        //Resume game logic here
    }
    void MoveToHome()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
    void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit!!!");
    }

    public void CoinCollected()
    {
        coinCollected++;
        ShowCoins();
    }
    void ShowCoins()
    {
        coins.text = "Coins : " + coinCollected;
    }



}//class
