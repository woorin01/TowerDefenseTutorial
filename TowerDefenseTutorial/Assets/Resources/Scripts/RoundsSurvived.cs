using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RoundsSurvived : MonoBehaviour
{
    public Text roundsText;

    private void OnEnable()
    {
        StartCoroutine("AnimateText");
    }

    private IEnumerator AnimateText()
    {
        roundsText.text = "0";
        yield return new WaitForSeconds(1f);

        for (int i = 0; i <= PlayerStats.rounds; i++)
        {
            roundsText.text = i.ToString();
            yield return new WaitForSeconds(5f * Time.deltaTime);
        }
    }

}
