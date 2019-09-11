using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public TurretBlueprint TurretToBuild { get; set; }
    private Node selectedNode;

    public GameObject buildEffect;
    public GameObject sellEffect;
    public NodeUI nodeUI;
    public bool CanBuild
    {
        get
        {
            return TurretToBuild != null;
        }
    }
    public bool HasMoney
    {
        get
        {
            return PlayerStats.money >= TurretToBuild.cost;
        }
    }

    public static BuildManager instance;
    private void Awake()
    {
        if (instance != null)
            return;

        instance = this;
    }

    public void SelectNode(Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        TurretToBuild = null;

        nodeUI.SetTarget(node);
    }

    public void SelectTurretToBuild(TurretBlueprint tower)
    {
        TurretToBuild = tower;
        DeselectNode();
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

}
