using UnityEngine;
using UnityEngine.SceneManagement;

public class Paused : MonoBehaviour
{
    public GameObject UI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            Toggle();
    }

    public void Toggle()
    {
        UI.SetActive(!UI.activeSelf);//ui가 켜져있다면 끄고, 꺼져있다면 킨다 

        if (UI.activeSelf)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }

    public void RetryButton()
    {
        Time.timeScale = 1f;
        SceneFader.instance.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void MenuButton()
    {
        Time.timeScale = 1f;
        SceneFader.instance.FadeTo("MainMenu");
    }

}
