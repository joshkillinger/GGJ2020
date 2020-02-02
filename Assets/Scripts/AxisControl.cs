using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AxisControl : MonoBehaviour
{
    public string[] Axes = { "Player1_Horizontal_K", "Player1_Horizontal_G" };

    public float Speed = 1;

    [SerializeField]
    public UnityEventFloat onChange = new UnityEventFloat();

    // Update is called once per frame
    private void FixedUpdate()
    {
        foreach (var axis in Axes)
        {
            var value = Input.GetAxis(axis);

            if (Mathf.Abs(value) > 0.05)
            {
                value *= Speed * Time.fixedDeltaTime;
                onChange.Invoke(value);
                return;
            }
        }
    }
}
