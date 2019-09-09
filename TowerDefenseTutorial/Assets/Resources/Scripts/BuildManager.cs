using UnityEngine;

public class BuildManager : MonoBehaviour
{
    private TowerBlueprint turretToBuild;
    private Node selectedNode;

    public GameObject buildEffect;
    public NodeUI nodeUI;
    public bool CanBuild
    {
        get
        {
            return turretToBuild != null;
        }
    }
    public bool HasMoney
    {
        get
        {
            return PlayerStats.money >= turretToBuild.cost;
        }
    }


    public static BuildManager instance;
    private void Awake()
    {
        if (instance != null)
            return;

        instance = this;
    }

    public void BuildTowerOn(Node node)
    {
        if (PlayerStats.money < turretToBuild.cost)
            return;

        PlayerStats.money -= turretToBuild.cost;

        GameObject temp = Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.tower = temp;
        node.MeshRend.material.color = Color.red;

        temp = Instantiate(buildEffect, node.GetBuildPosition(), Quaternion.identity);
        Destroy(temp, 4f);
    }

    public void SelectNode(Node node)
    {
        if(selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    public void SelectTurretToBuild(TowerBlueprint tower)
    {
        turretToBuild = tower;
        DeselectNode();
    }

}
