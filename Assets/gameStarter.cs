using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameStarter : MonoBehaviour
{
    private explodything boomBoom;
    private startingFanfare fanfare;
    private GameKeeper gameKeeper;
    private void init()
    {
        boomBoom = FindObjectOfType<explodything>();
        fanfare = FindObjectOfType<startingFanfare>();
        gameKeeper = FindObjectOfType<GameKeeper>();

        if(boomBoom == null)
        {
            Debug.LogError("NO BOOM BOOM");
        }

        if(fanfare == null)
        {
            Debug.LogError("NO FANFARE");
        }

        if(gameKeeper == null)
        {
            Debug.LogError("NO GAME KEEPER");
        } 
    }
    

    private void Awake()
    {
        init();
    }
    IEnumerator Start()
    {
        yield return StartCoroutine(fanfare.Fanfare());
        boomBoom.doTheBoomBoom();
        gameKeeper.startGame();
    }
}
