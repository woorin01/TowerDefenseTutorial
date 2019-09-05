using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.IO;
using UnityEngine.Networking;

public class LoadAssetBundleExample : MonoBehaviour
{
    public string prefabBundleURL = "file:\\" + "C:\\Users\\woori\\OneDrive\\바탕 화면\\TowerDefenseTutorial\\TowerDefenseTutorial\\AssetBundles\\StandaloneWindows\\prefabs";
    public string materialBundleURL = "file:\\" + "C:\\Users\\woori\\OneDrive\\바탕 화면\\TowerDefenseTutorial\\TowerDefenseTutorial\\AssetBundles\\StandaloneWindows\\materials";
    public string sceneBundleURL = "file:\\" + "C:\\Users\\woori\\OneDrive\\바탕 화면\\TowerDefenseTutorial\\TowerDefenseTutorial\\AssetBundles\\StandaloneWindows\\scenes";

    public static LoadAssetBundleExample instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    IEnumerator Start()
    {
        yield return StartCoroutine(DownloadAllAssetBundle());

        //AssetBundle a = AssetBundleManager.getAssetBundle(prefabBundleURL, 0);
        //AssetBundleRequest abr = a.LoadAssetAsync<GameObject>("Tower");
        //Instantiate(abr.asset);

        //a = AssetBundleManager.getAssetBundle(materialBundleURL, 0);
        //abr = a.LoadAssetAsync<Material>("Enemy");
        //temp.GetComponent<MeshRenderer>().material = abr.asset as Material;

    }

    IEnumerator DownloadAllAssetBundle()
    {
        yield return StartCoroutine(AssetBundleManager.DownloadAssetBundle(prefabBundleURL, 0));
        yield return StartCoroutine(AssetBundleManager.DownloadAssetBundle(materialBundleURL, 0));
        yield return StartCoroutine(AssetBundleManager.DownloadAssetBundle(sceneBundleURL, 0));

    }

    IEnumerator LoadAssetBundleScene()
    {
        if (AssetBundleManager.GetAssetBundle(sceneBundleURL, 0).isStreamedSceneAssetBundle)
        {
            string[] scenePaths = AssetBundleManager.GetAssetBundle(sceneBundleURL, 0).GetAllScenePaths();
            string sceneName = Path.GetFileNameWithoutExtension(scenePaths[1]);

            SceneManager.LoadScene(sceneName);
        }
        yield return null;
    }
    public void Button()
    {
        if(AssetBundleManager.GetAssetBundle(sceneBundleURL, 0))
            StartCoroutine(LoadAssetBundleScene());
    }
    public void Button2()
    {
        if (AssetBundleManager.GetAssetBundle(prefabBundleURL, 0))
        {
            AssetBundle a = AssetBundleManager.GetAssetBundle(prefabBundleURL, 0);
            AssetBundleRequest abr = a.LoadAssetAsync<GameObject>("Tower");
            Instantiate(abr.asset, new Vector3(15, 2, 75), Quaternion.identity, null);
        }
    }
    void OnDisable()
    {
        AssetBundleManager.Unload(prefabBundleURL, 0, false);
    }
}
