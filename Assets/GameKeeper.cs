using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameKeeper : MonoBehaviour
{
    public string gameMode = "Towahs";
    public ScoreZone player1Box;
    public ScoreZone player2Box;

    public Text player1Score;
    public Text player2Score;

    public Text timer;
    public int gameSeconds;
    private float gameStartTime;
    private float currentTime;

    private bool gameStarted = false;
    private bool gameEnded = false;

    public Text gameEndText;


    public void startGame()
    {
        gameStartTime = Time.time;
        gameStarted = true;
    }

    private void handleTimer()
    {
        currentTime = Time.time;
        int passedSeconds = Mathf.RoundToInt(currentTime - gameStartTime);
        int remainingSeconds = (gameSeconds - passedSeconds);
        int remainingMinutes =  remainingSeconds / 60;
        int remainder = remainingSeconds % 60;
        if (remainingSeconds > 0)
        {
            timer.text = remainingMinutes.ToString() + ":" + remainder.ToString("D2");
        }
        else
        {
            gameEnded = true;
        }
    }

    private void handleBloxScore()
    {
        player1Score.text = "P1 SCORE: " + player1Box.getNumBlocks().ToString();
        player2Score.text = "P2 SCORE: " + player2Box.getNumBlocks().ToString();


    }

    private void handleTowahsScore()
    {
        player1Score.text = "P1 Height: " + player1Box.getHeightScore().ToString("F2") + "m";
        player2Score.text = "P2 Height: " + player2Box.getHeightScore().ToString("F2") + "m";
    }

    private IEnumerator handleGameEnding()
    {
        gameStarted = false;
        if(player1Box.getHeightScore() > player2Box.getHeightScore())
        {
            gameEndText.text = "Player 1 wins";
        }
        else
        {
            gameEndText.text = "Player 2 wins";
        }
        yield return null;
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
    }

    void Update()
    {
        if (gameEnded)
        {
            StartCoroutine(handleGameEnding());
        }
        if (gameStarted)
        {
            handleTimer();
            if (gameMode == "Blox")
            {
                handleBloxScore();
            }
            else if(gameMode == "Towahs")
            {
                handleTowahsScore();
            }
        }
    }
}
