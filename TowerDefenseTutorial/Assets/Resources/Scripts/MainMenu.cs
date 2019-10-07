using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public string sceneToLoad = "LevelSelect";

    public void Play()
    {
        SceneFader.instance.FadeTo(sceneToLoad);
    }

    public void Quit()
    {
        Application.Quit(0);
    }

}
