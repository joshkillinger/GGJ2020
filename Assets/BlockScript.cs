using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    private bool isGrabbed = false;

    public void grabBlock() { isGrabbed = true; }
    public void ungrabBlock() { isGrabbed = false; }

    public bool getIsGrabbed() { return isGrabbed; }
}
