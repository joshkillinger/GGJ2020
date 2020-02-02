using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenStartGame : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadControls()
    {
        SceneManager.LoadScene(3);
    }
}
