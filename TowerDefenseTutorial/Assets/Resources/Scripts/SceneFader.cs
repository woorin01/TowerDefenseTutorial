using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    public static SceneFader instance;//SingleTone

    public Image fadeImage;
    public AnimationCurve fadeCurve;
    public int scenePrafabNum;
    public bool isFadeOut;

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
        if (SceneManager.GetActiveScene().name.Equals("Level"))
            Instantiate(Resources.Load<GameObject>("Prefabs/Level" + scenePrafabNum.ToString()));
        StartCoroutine("FadeIn");
    }

    public void FadeTo(string sceneName)
    {
        if (!isFadeOut)
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
        isFadeOut = true;
        Color c = fadeImage.color;
        for (float i = 0f; i <= 1f; i += 0.01f)
        {
            float a = fadeCurve.Evaluate(i);
            fadeImage.color = new Color(c.r, c.g, c.b, a);

            yield return new WaitForSeconds(Time.deltaTime * 0.1f);
        }
        isFadeOut = false;

        SceneManager.LoadScene(sceneName);
    }
}
