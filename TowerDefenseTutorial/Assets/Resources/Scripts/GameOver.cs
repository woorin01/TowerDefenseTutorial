﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text roundsText;

    private void OnEnable()
    {
        roundsText.text = PlayerStats.rounds.ToString();
    }

    public void Retry()
    {
        FindObjectOfType<SceneFader>().FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        FindObjectOfType<SceneFader>().FadeTo("MainMenu");
    }
}
