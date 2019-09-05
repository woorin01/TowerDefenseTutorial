using System;
using UnityEngine;
using System.Collections;
public class CachingDownloadExample : MonoBehaviour
{
    public string BundleURL = "file:\\" + "C:\\Users\\woori\\OneDrive\\바탕 화면\\TowerDefenseTutorial\\TowerDefenseTutorial\\AssetBundles\\StandaloneWindows\\prefabs";

    void Start()
    {
        StartCoroutine(DownloadAndCache());
    }
    IEnumerator DownloadAndCache()
    {
        while (!Caching.ready)
            yield return null;

        using (WWW www = WWW.LoadFromCacheOrDownload(BundleURL, 0))
        {
            yield return www;
            if (www.error != null)
                throw new Exception("WWW 다운로드에 에러가 생겼습니다.:" + www.error);
        }
    }
}
