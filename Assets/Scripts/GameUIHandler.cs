using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUIHandler : MonoBehaviour
{


    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI endGameText;
    public TextMeshProUGUI highScoreText;

    private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        gm.OnScoreChange += UpdateGameUI;
        gm.OnGameEnd += EndGameUI;
        gm.OnHighScoreChange += UpdateHightScore;
        UpdateHightScore();
    }

    // Update is called once per frame


    public void UpdateGameUI()
    {
        scoreText.SetText("Score : " + gm.score);
    }

    public void EndGameUI()
    {
        endGameText.gameObject.SetActive(true);
    }

    public void UpdateHightScore()
    {
        highScoreText.SetText("Highscore : " + gm.highScore.score);
    }

    private void OnDestroy()
    {
        gm.OnScoreChange -= UpdateGameUI;
        gm.OnGameEnd -= EndGameUI;
    }
}
