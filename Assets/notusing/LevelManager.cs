using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    public static LevelManager Instace{get {return instance;}}
    public string level1;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }else
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        if(GetLevelStstus(level1) == LevelStatus.Locked)
        {
            SetLevelStatus(level1,LevelStatus.Unlocked);
        }
    }
    public LevelStatus GetLevelStstus(string level)
    {
        LevelStatus levelStatus = (LevelStatus) PlayerPrefs.GetInt(level,0);
        return levelStatus;
    }

    public void SetLevelStatus(string level, LevelStatus levelStatus)
    {
        PlayerPrefs.SetInt(level ,(int)levelStatus);
    }
}
