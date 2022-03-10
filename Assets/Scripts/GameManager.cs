using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;


#if UNITY_EDITOR
using UnityEditor;
#endif


[System.Serializable]
public struct HighScoreEntry
{
    public int score;

    public HighScoreEntry(int Score)
    {
        score = Score;
    }
}
public class GameManager : MonoBehaviour
{

    public int score = 0;
    public HighScoreEntry highScore = new HighScoreEntry(0);

    public PlayerController playerController;

    public static GameManager Instance;


    public delegate void OnScoreChangeDelegate();
    public OnScoreChangeDelegate OnScoreChange;

    public delegate void OnGameEndDelegate();
    public OnScoreChangeDelegate OnGameEnd;

    public delegate void OnHighScoreChangeDelegate();
    public OnHighScoreChangeDelegate OnHighScoreChange;

    public bool endGame = false;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        LoadScoreData();
    }

    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        playerController.OnPlayerDeath += EndGame;
    }

   
    void Update()
    {
        RestartGame();
    }

    public void AddScore(int amount)
    {
        if(!playerController.isDead)
        {
            score += amount;
            OnScoreChange?.Invoke();
        }
    }

    public void UpdateHighScore()
    {
        if(highScore.score < score)
        {
            highScore = new HighScoreEntry(score);
            OnHighScoreChange?.Invoke();
        }
    }

    public void EndGame()
    {
        endGame = true;
        UpdateHighScore();
        OnGameEnd?.Invoke();
    }

    public void RestartGame()
    {
        if(Input.GetKeyDown(KeyCode.R) && endGame)
        {
            AddScore(-score);
            SaveScoreData();
            SceneManager.LoadScene(0);
        }
        else if(Input.GetKeyDown(KeyCode.Q) && endGame)
        {
            SaveScoreData();
            QuitGame();
        }
    }


    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    [System.Serializable]
    class SaveData
    {
        public HighScoreEntry highestScore;
    }

    public void SaveScoreData()
    {
        SaveData data = new SaveData();
        data.highestScore = highScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScoreData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScore = data.highestScore;
        }
    }
}
