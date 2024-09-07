using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]

public class LevelLoad : MonoBehaviour
{
       private Button button;
    public string levelName;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        LevelStatus levelStatus = LevelManager.Instace.GetLevelStstus(levelName);
        switch (levelStatus)
    {
        case LevelStatus.Locked:
        Debug.Log("Level is locked");
            break;
        case LevelStatus.Unlocked:
        SceneManager.LoadScene(levelName);
            break;
        case LevelStatus.Completed:
        SceneManager.LoadScene(levelName);
            break;
        default:
            break;

    }
        SceneManager.LoadScene(levelName);
    }
    
}
