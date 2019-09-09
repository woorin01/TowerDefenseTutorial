using UnityEngine;

public class BuildManager : MonoBehaviour
{
    private TowerBlueprint towerToBuild;

    public bool CanBuild
    {
        get
        {
            return towerToBuild != null;
        }
    }
    public bool HasMoney
    {
        get
        {
            return PlayerStats.money >= towerToBuild.cost;
        }
    }

    public GameObject buildEffect;

    public static BuildManager instance;
    private void Awake()
    {
        if (instance != null)
            return;

        instance = this;
    }

    public void BuildTowerOn(Node node)
    {
        if (PlayerStats.money < towerToBuild.cost)
            return;

        PlayerStats.money -= towerToBuild.cost;

        GameObject temp = Instantiate(towerToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.tower = temp;
        node.MeshRend.material.color = Color.red;

        temp = Instantiate(buildEffect, node.GetBuildPosition(), Quaternion.identity);
        Destroy(temp, 4f);
    }

    public void SelectTurretToBuild(TowerBlueprint tower)
    {
        towerToBuild = tower;
    }

}
