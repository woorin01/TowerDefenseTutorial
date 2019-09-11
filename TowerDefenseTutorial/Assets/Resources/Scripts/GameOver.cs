using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void Retry()
    {
        SceneFader.instance.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        SceneFader.instance.FadeTo("MainMenu");
    }
}
