using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D physics = null;
    [SerializeField] private FixedJoint2D joint = null;
    [SerializeField] private List<Transform> grabPoints = null;

    private Rigidbody2D rb;
    private float steadyTime = .5f;
    private float timeIveBeenSteady = 0f;
    private float steadyVelocity = .1f;
    private float steadyAngularVelocity = .1f;
    public bool IsGrabbed
    {
        get;
        private set;
    }

    public bool IsSteady
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

        Debug.Log(closest.gameObject);

        transform.localRotation = closest.localRotation;
        joint.connectedAnchor = new Vector2(0, closest.GetComponent<Offset>().Value);
        joint.connectedBody = grabber;
        joint.enabled = true;
        physics.freezeRotation = true;
        IsGrabbed = true;
    }

    public void Release()
    {
        joint.connectedBody = null;
        joint.enabled = false;
        physics.freezeRotation = false;
        IsGrabbed = false;
    }

    public void grabBlock() { IsGrabbed = false; }
    public void ungrabBlock() { IsGrabbed = true; }

    public bool getIsGrabbed() { return IsGrabbed; }

    void Awake()
    {
        IsGrabbed = false;
        IsSteady = true;
        rb = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float v = rb.velocity.sqrMagnitude;
        float w = rb.angularVelocity;
        if (v < steadyVelocity && w < steadyAngularVelocity)
        {
            timeIveBeenSteady += Time.deltaTime;
            if(timeIveBeenSteady > steadyTime)
            {
                IsSteady = true;
            }
        }
        else
        {
            IsSteady = false;
            timeIveBeenSteady = 0;
        }
    }
}
