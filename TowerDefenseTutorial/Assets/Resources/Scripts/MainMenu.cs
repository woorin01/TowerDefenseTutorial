﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string sceneToLoad = "LevelSelect";

    public void Play()
    {
        FindObjectOfType<SceneFader>().FadeTo(sceneToLoad);
    }

    public void Quit()
    {
        Application.Quit(0);
    }

}
