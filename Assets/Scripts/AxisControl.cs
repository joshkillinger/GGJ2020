using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AxisControl : MonoBehaviour
{
    public string Axis = "Player1_Horizontal";

    public float Speed = 1;

    [SerializeField]
    public UnityEventFloat onChange = new UnityEventFloat();

    // Update is called once per frame
    private void FixedUpdate()
    {
        var value = Input.GetAxis(Axis);

        if (Mathf.Abs(value) > 0.05)
        {
            value *= Speed * Time.fixedDeltaTime;
            onChange.Invoke(value);
            return;
        }
    }
}
