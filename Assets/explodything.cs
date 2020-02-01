using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//here's a thing that makes a little poofy force at the begginning of the game to knock the tower over : )
public class explodything : MonoBehaviour
{
    public float FORCE;
    private List<Rigidbody2D> thingsToForce;
    private Vector3 whereIAm;

    private void Awake()
    {
        whereIAm = this.transform.position;
        thingsToForce = new List<Rigidbody2D>();
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("blocks");
        foreach (GameObject block in blocks)
        {
            Rigidbody2D rb = block.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                thingsToForce.Add(rb);
            }
        }
    }


    public void doTheBoomBoom()
    {
        foreach (Rigidbody2D rb in thingsToForce)
        {
            Vector3 pos = rb.transform.position;
            Vector3 posDiff = pos - whereIAm;
            rb.AddForceAtPosition(FORCE * posDiff, pos, ForceMode2D.Impulse);
        }
    }
    
}
