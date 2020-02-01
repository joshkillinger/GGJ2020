using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalControl : MonoBehaviour
{
    public float Min;
    public float Max;

    public float Speed;

    // Update is called once per frame
    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");

        if (Mathf.Abs(horizontal) > 0.05)
        {
            var x = horizontal * Speed * Time.deltaTime;
            x = x + transform.localPosition.x;
            x = Mathf.Clamp(x, Min, Max);
            transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
        }
    }
}
