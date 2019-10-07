﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool gameIsOver = false;
    public static int enemiesAlive = 0;

    public GameObject gameOverUI;
    public GameObject completeLevelUI;
    public GameObject spawnPoint;
    public Text waveCountText;
    public Wave[] waves;
    public string nextLevelSceneName;
    public int levelToUnlock;

    public float timeBetweenWaves = 5f;
    private float waveCount = 2f;

    private int waveNumber = 0;

    void Start()
    {
        //StartCoroutine("WaveCountDown");
        gameIsOver = false;
    }

    private void Update()
    {
        if (gameIsOver)
            return;

        if (Input.GetKeyDown(KeyCode.E))
            EndGame();

        if (PlayerStats.lives <= 0)
            EndGame();

        WaveCount();//왼쪽 아래에 카운트다운 표시 및 적소환
    }

    private void WaveCount()
    {
        if (enemiesAlive > 0)
            return;

        if (waveNumber.Equals(waves.Length))
        {
            WinLevel();
            enabled = false;
        }

        if (waveCount <= 0f)
        {
            StartCoroutine("SpawnEnemy");
            waveCount = timeBetweenWaves;

            return;
        }
        waveCount -= Time.deltaTime;
        waveCount = Mathf.Clamp(waveCount, 0f, Mathf.Infinity);
        waveCountText.text = string.Format("{0:00.00}", waveCount);//소수점까지 보여주는 카운트다운
        //waveCountText.text = Mathf.Floor(waveCount).ToString();//소수점은 보여주지 않는 카운트다운
    }

    //IEnumerator WaveCountDown()
    //{
    //    waveCountText.text = waveCount.ToString();
    //    for (int i = waveCount; i > 0; i--)
    //    {
    //        waveCountText.text = i.ToString();
    //        yield return new WaitForSeconds(1f);
    //    }
    //    waveCount = 5;

    //    StartCoroutine("WaveCountDown");
    //    StartCoroutine("SpawnEnemy");
    //}

    IEnumerator SpawnEnemy()
    {
        PlayerStats.rounds++;

        Wave wave = waves[waveNumber];
        enemiesAlive = wave.count;

        for (int i = 0; i < wave.count; i++)
        {
            MakeEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }
        waveNumber++;
        Debug.Log(waveNumber.Equals(waves.Length));
    }

    private void MakeEnemy(GameObject enemy)
    {
        Instantiate<GameObject>(enemy, spawnPoint.transform.position, Quaternion.identity);
    }

    private void EndGame()
    {
        gameIsOver = true;
        gameOverUI.SetActive(true);
        Debug.Log("Game Over!");
    }

    public void WinLevel()
    {
        if (PlayerPrefs.GetInt("levelReached") < levelToUnlock)
            PlayerPrefs.SetInt("levelReached", levelToUnlock);
        SceneFader.instance.scenePrafabNum = levelToUnlock;

        gameIsOver = true;
        completeLevelUI.SetActive(true);
    }

}
