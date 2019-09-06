using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject spawnPoint;
    public Text waveCountText;
    public float waveCount;
    private int waveNumber = 1;

    void Start()
    {
        //StartCoroutine("WaveCountDown");
        enemyPrefab = Resources.Load<GameObject>("Prefabs/Enemy");
    }

    private void Update()
    {
        if(waveCount <= 0f)
        {
            StartCoroutine("SpawnEnemy");
            waveCount = 5f;
        }
        waveCount -= Time.deltaTime;
        waveCount = Mathf.Clamp(waveCount, 0f, Mathf.Infinity);
        waveCountText.text = string.Format("{0:00.00}", waveCount);//소숫점까지 보여주는 카운트다운
        //waveCountText.text = Mathf.Floor(waveCount).ToString();//소숫점은 보여주지 않는 카운트다운
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
        for (int i = 0; i < waveNumber; i++)
        {
            MakeEnemy();
            yield return new WaitForSeconds(0.5f);
        }
        waveNumber++;
    }
    private void MakeEnemy()
    {
        Instantiate<GameObject>(enemyPrefab, spawnPoint.transform.position, Quaternion.identity);
    }
}
