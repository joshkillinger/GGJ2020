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

    private void grab() {
        
        if (!isGrabbing) {
            Debug.Log("grabbing");
            claw.isGrasping = true;
        }
        isGrabbing = true;
        if(currentBlock != null)
        {
            //clench that block
            freezeBlockRotation();
        }
    }
    private void ungrab() {
        if (isGrabbing){
            Debug.Log("ungrabbing");
            claw.isGrasping = false;
            if(currentBlock != null)
            {
                //let it go, let it go
                currentBlock.velocity = cranePulleyClawRigidBody.velocity;
                unfreezeBlockRotation();
            }
        }
        isGrabbing = false;
    }

    private void freezeBlockRotation() { currentBlock.freezeRotation = true; }
    private void unfreezeBlockRotation() { currentBlock.freezeRotation = false; }

    private void moveGrabbedBlock()
    {
        if (isGrabbing && currentBlock != null)
        {
            currentBlock.transform.position = grabPoint.position;     
        }
    }

    private void Update()
    {
        if (PlayerNumber == 1)
        {
            if (Input.GetAxis("Player1_Grab") > Mathf.Epsilon)
            {
                grab();
            }
            else
            {
                ungrab();
            }
            moveGrabbedBlock();
        }
        else if(PlayerNumber == 2)
        {
            if (Input.GetAxis("Player2_Grab") > Mathf.Epsilon)
            {
                grab();
            }
            else
            {
                ungrab();
            }
            moveGrabbedBlock();
        }
    }



}
