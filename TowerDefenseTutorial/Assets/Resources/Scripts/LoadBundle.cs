using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadBundle : MonoBehaviour
{
    IEnumerator Start()
    {
        //Change url for you asset bundle path
        string url = "C:\\Users\\woori\\OneDrive\\바탕 화면\\TowerDefenseTutorial\\TowerDefenseTutorial\\AssetBundles\\StandaloneWindows\\prefabs";

        WWW www = new WWW("file:\\" + url);
        while (!www.isDone)
        {
            yield return null;
        }

        AssetBundle testbundle = www.assetBundle;
        if (testbundle != null)
        {
            GameObject tower = testbundle.LoadAsset<GameObject>("Tower");
            tower.transform.position = Vector3.zero;
            Instantiate(tower);
        }
        else
        {
            Debug.LogError("Asset Bundle Is Null");
        }
    }

}
