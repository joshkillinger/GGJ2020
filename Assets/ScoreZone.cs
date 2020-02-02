using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreZone : MonoBehaviour
{
    private int numBlocksInZone;
    private List<GameObject> blocksInZone;
    private float heightScore;
    private float floor;
    public Transform floorMarker; 

    public int getNumBlocks() { return numBlocksInZone; }
    public float getHeightScore() { return heightScore; }
    private void Awake()
    {
        heightScore = 0f;
        numBlocksInZone = 0;
        blocksInZone = new List<GameObject>();
        floor = floorMarker.position.y;
    }

    private float determineHighestBlock()
    {
        float mostHighestHeight = 0;
        foreach(GameObject obj in blocksInZone)
        {
            if(obj != null)
            {
                BlockScript bs = obj.GetComponent<BlockScript>();
                if (bs != null)
                {
                    if (!bs.IsGrabbed)
                    {
                        if (obj.transform.position.y > mostHighestHeight)
                        {
                            mostHighestHeight = Mathf.Max(obj.transform.position.y - floor, 0f);
                        }
                    }
                }
            }
        }
        return mostHighestHeight;
    }

    private void Update()
    {
        heightScore = determineHighestBlock();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "blocks")
        {
            numBlocksInZone++;            
            blocksInZone.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "blocks")
        {
            numBlocksInZone--;
            blocksInZone.Remove(collision.gameObject);
        }
    }
}
