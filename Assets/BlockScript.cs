using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D physics = null;
    [SerializeField] private FixedJoint2D joint = null;
    [SerializeField] private List<Transform> grabPoints = null;

    public bool IsGrabbed
    {
        get;
        private set;
    }

    public void Grab(Rigidbody2D grabber)
    {
        var grabberpos = grabber.transform.position;

        float closestDist = float.PositiveInfinity;
        Transform closest = null;
        foreach (var point in grabPoints)
        {
            var dist = (grabberpos - point.position).sqrMagnitude;
            if (dist < closestDist)
            {
                closest = point;
                closestDist = dist;
            }
        }

        joint.connectedBody = grabber;
        joint.enabled = true;
        IsGrabbed = true;
    }

    public void Release()
    {
        joint.connectedBody = null;
        joint.enabled = false;
        IsGrabbed = false;
    }
}
