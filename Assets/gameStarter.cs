using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameStarter : MonoBehaviour
{
    private explodything boomBoom;
    private startingFanfare fanfare;
    private void init()
    {
        boomBoom = FindObjectOfType<explodything>();
        fanfare = FindObjectOfType<startingFanfare>();

        if(boomBoom == null)
        {
            Debug.LogError("NO BOOM BOOM");

        }

        if(fanfare == null)
        {
            Debug.LogError("NO FANFARE");
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
    }
}
