using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public static SceneFader instance;

    public Image fadeImage;
    public AnimationCurve fadeCurve;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        StartCoroutine("FadeIn");
    }
    private void OnLevelWasLoaded(int level)
    {
        StartCoroutine("FadeIn");
    }

    public void FadeTo(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }

    private IEnumerator FadeIn()
    {
        Color c = fadeImage.color;
        fadeImage.color = new Color(c.r, c.g, c.b, 1f);
        for (float i = 1f; i >= 0f; i -= 0.01f)
        {
            float a = fadeCurve.Evaluate(i);
            fadeImage.color = new Color(c.r, c.g, c.b, a);

            yield return new WaitForSeconds(Time.deltaTime * 0.1f);
        }
    }

    private IEnumerator FadeOut(string sceneName)
    {
        Color c = fadeImage.color;
        for (float i = 0f; i <= 1f; i += 0.01f)
        {
            float a = fadeCurve.Evaluate(i);
            fadeImage.color = new Color(c.r, c.g, c.b, a);

            yield return new WaitForSeconds(Time.deltaTime * 0.1f);
        }

        SceneManager.LoadScene(sceneName);
    }
}
