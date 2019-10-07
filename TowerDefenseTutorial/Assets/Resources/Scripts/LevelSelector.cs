using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public Button[] levelButtons;

    private void Awake()
    {
        //PlayerPrefs.SetInt("levelReached", 1);
        int levelReached = PlayerPrefs.GetInt("levelReached");

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelReached)
                levelButtons[i].interactable = false;
        }

    }

    public void Select(int scenePrefabNum)
    {
        SceneFader.instance.scenePrafabNum = scenePrefabNum;
        SceneFader.instance.FadeTo("Level");
    }

}
