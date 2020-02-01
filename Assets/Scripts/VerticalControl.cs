using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalControl : MonoBehaviour
{
    public float Min;
    public float Max;

    public float Speed;

    // Update is called once per frame
    void Update()
    {
        var vertical = Input.GetAxis("Vertical");

        if (Mathf.Abs(vertical) > 0.05)
        {
            var y = vertical * Speed * Time.deltaTime;
            y = y + transform.localPosition.y;
            y = Mathf.Clamp(y, Min, Max);
            transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
        }
    }
}
