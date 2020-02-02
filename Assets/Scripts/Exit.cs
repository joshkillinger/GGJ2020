using UnityEngine;

public class Exit : MonoBehaviour
{
    public void Event_OnClick()
    {
#if UNITY_EDITOR
        Debug.Log("Exit");
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit(0);
        #endif
    }
}
