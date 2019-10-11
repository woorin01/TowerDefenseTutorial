using System;
using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
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

    private float doubleClickTime = 0f;

    public delegate void Test();
    public Test test;

    public Text time;
    public float max = 30f;
    void Start()
    {
        Observable.EveryUpdate()
            .ThrottleFirst(TimeSpan.FromSeconds(1))
            .Where(_ => max > 0)
            .Subscribe(x =>
            {
                time.text = (max -= 1).ToString();
            }, _ =>
            {
            }, _ =>
            {
            });

        //StartCoroutine("WaveCountDown");
        gameIsOver = false;

        Observable.EveryUpdate()//매 프레임의 메세지를 스트림에 싣고
             .Where(_ => Input.GetMouseButton(1))//그 메세지중 클릭이 있던 프레임의 메세지만 통과
             .ThrottleFirst(TimeSpan.FromSeconds(0.2f))//첫 메세지는 받고 그 뒤에 오는 메세지는 0.2초간 무시 (0.2초가 지나면 다시 받음)
             .Subscribe(_ => Debug.Log("What The Fuck"));

        //아래(업데이트 안에 넣고)랑 위랑 똑같음

        if (doubleClickTime > 0f)
            doubleClickTime -= Time.deltaTime;

        if (Input.GetMouseButton(1))
            if (EventSystem.current.IsPointerOverGameObject().Equals(false))
            {
                if (doubleClickTime <= 0f)
                {
                    Debug.Log("first click");
                    doubleClickTime = 1f;
                }

                else if (doubleClickTime <= 1f - 0.2f)
                {
                    //test();
                    test = null;
                    Debug.Log("double click");
                    doubleClickTime = 0f;
                }
            }
    }

    private void Update()
    {
        if (doubleClickTime > 0f)
            doubleClickTime -= Time.deltaTime;

        if (Input.GetMouseButtonDown(1))
            if (EventSystem.current.IsPointerOverGameObject().Equals(false))
            {
                if (doubleClickTime <= 0f)
                {
                    Debug.Log("first click");
                    doubleClickTime = 0.5f;
                }
                else
                {
                    //test();
                    test = null;
                    Debug.Log("double click");
                    doubleClickTime = 0f;
                }
            }

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

    private void MakeEnemy(GameObject enemy) { GameObject g = Instantiate<GameObject>(enemy, spawnPoint.transform.position, Quaternion.identity); test += g.GetComponent<Enemy>().Die; }

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
