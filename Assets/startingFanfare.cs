using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class startingFanfare : MonoBehaviour
{

    private Text startText;

    void Awake()
    {
        startText = this.GetComponent<Text>();
    }
    public IEnumerator Fanfare()
    {
        yield return new WaitForSeconds(1f);
        startText.text = "2";
        yield return new WaitForSeconds(1f);
        startText.text = "1";
        yield return new WaitForSeconds(1f);
        startText.text = "GO!";
        yield return new WaitForSeconds(.3f);
        startText.gameObject.SetActive(false);
        yield return null;
    }
}
