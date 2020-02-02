using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        timer.text = remainingMinutes.ToString() + ":" + remainder.ToString("D2");
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


    void Update()
    {
        if (gameStarted)
        {
            handleTimer();
            if (gameMode == "Blox")
            {
                handleBloxScore();
            }else if(gameMode == "Towahs"){
                handleTowahsScore();
            }
        }
    }
}
