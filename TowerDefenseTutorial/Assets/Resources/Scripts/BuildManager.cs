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
    

    public GameObject standardTurretPrefab;
    public GameObject MissileTurretPrefab;

    public static BuildManager instance;
    private void Awake()
    {
        if(instance != null)
            return;

        instance = this;
    }

    public void BuildTowerOn(Node node)
    {
        GameObject temp = Instantiate(towerToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.tower = temp;
    }

    public void SelectTurretToBuild(TowerBlueprint tower)
    {
        towerToBuild = tower;
    }

}
