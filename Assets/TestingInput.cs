using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingInput : MonoBehaviour
{
    private void Update()
    {
        float p1h = Input.GetAxis("Player1_Horizontal");
        float p1v = Input.GetAxis("Player1_Vertical");
        float p1g = Input.GetAxis("Player1_Grab");
        float p2h = Input.GetAxis("Player2_Horizontal");
        float p2v = Input.GetAxis("Player2_Vertical");
        float p2g = Input.GetAxis("Player2_Grab");
        Debug.Log("p1h: " + p1h + ", p1v: " + p1v + ", p1g: " + p1g + ", p2h: " + p2h + ", p2v: " + p2v + ", p2g: " + p2g);
    }



}
