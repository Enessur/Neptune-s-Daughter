using System;
using Script;
using UnityEngine;

public class SceneManager : Singleton<SceneManager>
{
    // public GameObject winItem;
    
    //dandik çözüm :c aklıma başka bişe gelmedi
    [SerializeField] private PlayerMovement playerLife;
    
    public GameObject loseItem;
    public GameObject menuItem;
    public Level[] levels;
    public Transform player;
    
    private int winCondition;

    public static Action OnSceneRestart;
    public  Action OnNextLevel;
    public static Action OnNextLevelCalled;
    //public  Action<Level>OnNextLevel1;

    private void Start()
    {
        //  themeAudioSource.Play();
      //  loseItem.SetActive(false);
        menuItem.SetActive(false);
        Menu();
        Debug.Log("ben startım");
        //LoadLevel();
    }


    public void LoadLevel()
    {
        menuItem.SetActive(false);
        Time.timeScale = 1f;

        if (levels.Length > 0)
        {
            var currentLvl = ES3.Load("LevelIndex", 0);
            if (currentLvl == levels.Length)
            {
                currentLvl = 0;
                ES3.Save("LevelIndex", 0);
            }

            // Deactivate the current level
            if (currentLvl - 1 >= 0 && currentLvl - 1 < levels.Length)
            {
                levels[currentLvl - 1].gameObject.SetActive(false);
            }

            levels[currentLvl].gameObject.SetActive(true);
            player.position = levels[currentLvl].playerStartPoint.position;
            OnNextLevel?.Invoke();
        }
    }


    public void Menu()
    {
        Time.timeScale = 0f;
        menuItem.SetActive(true);
        // biliyorum bu çok yanlış :c 
        playerLife.MaxHealth();
        loseItem.SetActive(false);
    }

    public void NextLevel()
    {
        SoundManager.Instance.PlaySound("NextLevel");
        var currentLevelIndex = ES3.Load("LevelIndex", 0);
        currentLevelIndex++;

        
        if (currentLevelIndex >= levels.Length)
        {
            currentLevelIndex = 0;
            ES3.Save("LevelIndex", currentLevelIndex);
            Debug.Log("oyun bitti");
            Replay();
        }

        ES3.Save("LevelIndex", currentLevelIndex);
        OnNextLevel?.Invoke();
        
        if (currentLevelIndex < levels.Length)
        {
            LoadLevel();
        }
    
        OnNextLevelCalled?.Invoke();
    }


    public void Replay()
    {
        ES3.Save("LevelIndex", 0);
        
        loseItem.SetActive(false);
        
        LoadLevel();
    }

    public static void RestartScene()
    {
        OnSceneRestart?.Invoke();
       
    }
    
    
    


    public void WinGame()
    {
        // winItem.SetActive(true);
        Time.timeScale = 0f;
    }


    public void LoseGame()
    {
        loseItem.SetActive(true);
        Time.timeScale = 0f;
        ES3.Save("LevelIndex", 0);
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].gameObject.SetActive(false);
        }
        SoundManager.Instance.PlaySound("End");
    }

    public void WinCondition()
    {
        winCondition++;
        Debug.Log(winCondition);
        if (winCondition > 7)
        {
            NextLevel();
            winCondition = 0;
        }
    }
    
    public void ResetWinCondition()
    {
        winCondition =0;
        
    }
     

    public void EndGameCondition()
    {
        winCondition++;
        Debug.Log(winCondition);
        if (winCondition > 5)
        { 
            LoseGame();
           winCondition = 0;
        }
    }

    public void QuitGame()
    {
        Debug.Log("qqq");
        Application.Quit();
    }
}