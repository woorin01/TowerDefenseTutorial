using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
    public Text moneyText;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("UpdateByCoroutine");
    }

    IEnumerator UpdateByCoroutine()
    {
        for(; ; )
        {
            moneyText.text = "$" + PlayerStats.money.ToString();
            yield return new WaitForSeconds(0.1f);
        }
    }
}
