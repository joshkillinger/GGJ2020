using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingInput : MonoBehaviour
{
    public float p1h;
    public float p1v;
    public float p1g;

    public float p2h;
    public float p2v;
    public float p2g;

    public bool gamepad = true;

    private void Update()
    {
        p1h = Input.GetAxis($"Player1_Horizontal_{(gamepad ? "G" : "K")}");
        p1v = Input.GetAxis($"Player1_Vertical_{(gamepad ? "G" : "K")}");
        p1g = Input.GetAxis("Player1_Grab");
        p2h = Input.GetAxis($"Player2_Horizontal_{(gamepad ? "G" : "K")}");
        p2v = Input.GetAxis($"Player2_Vertical_{(gamepad ? "G" : "K")}");
        p2g = Input.GetAxis("Player2_Grab");
    }



}
