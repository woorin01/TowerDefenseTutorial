using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CompleteLevel : MonoBehaviour
{
    public void Continue()
    {
        SceneFader.instance.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        SceneFader.instance.FadeTo("MainMenu");
    }
}
