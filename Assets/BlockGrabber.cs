using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGrabber : MonoBehaviour
{
    public int PlayerNumber;
    public Transform grabPoint;
    public Rigidbody2D cranePulleyClawRigidBody;
    private Rigidbody2D currentBlock;
    private bool isGrabbing;
    private CraneClaw claw;

    private float lastFrameInput = 0;

    public bool toggleGrab = true;
    private bool grabOn = false;


    private void Awake()
    {
        CraneClaw[] allClaws = GameObject.FindObjectsOfType<CraneClaw>();
        foreach (CraneClaw c in allClaws)
        {
            if(c.gameObject.layer == gameObject.layer) //get my claw
            {
                claw = c;
                break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "blocks")
        {
            Debug.Log("Touching a block");
            if (!isGrabbing)
            {
                Debug.Log("Current block setting to " + collision.gameObject);
                currentBlock = collision.attachedRigidbody;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "blocks")
        {
            Debug.Log("Block leaving trigger area");
            if(currentBlock != null)
            {
                if(collision.attachedRigidbody == currentBlock && !isGrabbing)
                {
                    currentBlock = null;
                }
            }
        }
    }

    private void grab()
    {
        if (!isGrabbing && currentBlock != null)
        {
            Debug.Log("grabbing");
            claw.isGrasping = true;
            isGrabbing = true;

            //clench that block
            BlockScript bs = currentBlock.GetComponent<BlockScript>();
            if (bs != null)
            {
                bs.Grab(cranePulleyClawRigidBody);
            }
        }
    }

    private void ungrab()
    {
        if (isGrabbing && currentBlock != null)
        {
            Debug.Log("ungrabbing");
            claw.isGrasping = false;
            isGrabbing = false;

            //let it go, let it go
            BlockScript bs = currentBlock.GetComponent<BlockScript>();
            if (bs != null)
            {
                bs.Release();
            }
        }
    }

    private void handleGrab()
    {
        if (!toggleGrab)
        {
            if (Input.GetAxis($"Player{PlayerNumber}_Grab") > Mathf.Epsilon)
            {
                grabOn = true;
            }
            else
            {
                grabOn = false;
            }
        }
        else
        {
            float thisFrameInput = Input.GetAxis("Player" + PlayerNumber + "_Grab");
            if (thisFrameInput - Mathf.Epsilon > lastFrameInput)
            {
                grabOn = !grabOn;
            }
            lastFrameInput = thisFrameInput;
        }

        if (grabOn)
        {
            grab();
        }
        else
        {
            ungrab();
        }
    }

    private void FixedUpdate()
    {
        handleGrab();
    }
}
