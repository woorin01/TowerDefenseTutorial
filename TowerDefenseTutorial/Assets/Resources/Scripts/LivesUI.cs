using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
    public Text livesText;
    // Start is called before the first frame update
    void Start()
    {
        livesText.text = PlayerStats.lives.ToString() + " LIVES";
        StartCoroutine("UpdateByCoroutine");
    }

    IEnumerator UpdateByCoroutine()
    {
        for (; ; )
        {
            livesText.text = PlayerStats.lives.ToString() + " LIVES";
            yield return new WaitForSeconds(0.1f);
        }
    }
}
