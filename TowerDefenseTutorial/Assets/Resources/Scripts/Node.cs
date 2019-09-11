using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color startColor;
    public Color hoverColor;
    public Vector3 positionOffSet;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    private MeshRenderer meshRend;
    public MeshRenderer MeshRend
    {
        get
        {
            return meshRend;
        }
        set
        {
            meshRend = value;
        }
    }//get set

    private void Start()
    {
        meshRend = GetComponent<MeshRenderer>();
        startColor = meshRend.material.color;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffSet;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (turret != null)
        {
            BuildManager.instance.SelectNode(this);
            return;
        }

        if (!BuildManager.instance.CanBuild)
            return;

        BuildTurret(BuildManager.instance.TurretToBuild);
    }

    private void BuildTurret(TurretBlueprint blueprint)
    {
        if (PlayerStats.money < blueprint.cost)
            return;

        PlayerStats.money -= blueprint.cost;

        GameObject temp = Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = temp;

        temp = Instantiate(BuildManager.instance.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(temp, 4f);

        turretBlueprint = BuildManager.instance.TurretToBuild;
        meshRend.material.color = Color.red;
    }

    public void UpgradeTurret()
    {
        if (PlayerStats.money < turretBlueprint.upgradeCost)
            return;

        PlayerStats.money -= turretBlueprint.upgradeCost;

        //이전 터렛 파괴
        Destroy(turret);

        //한 후에 업그레이드 타워 건설
        GameObject temp = Instantiate(turretBlueprint.upgradePrefab, GetBuildPosition(), Quaternion.identity);
        turret = temp;

        temp = Instantiate(BuildManager.instance.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(temp, 4f);

        isUpgraded = true;
    }

    public void SellTurret()
    {
        if (!isUpgraded)
            PlayerStats.money += turretBlueprint.GetSellAmount();
        else
        {
            PlayerStats.money += turretBlueprint.GetSellAmountIsUpgraded();
            isUpgraded = false;
        }

        Destroy(turret);
        turretBlueprint = null;

        GameObject temp = Instantiate(BuildManager.instance.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(temp, 4f);
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!BuildManager.instance.CanBuild)
            return;

        if (BuildManager.instance.HasMoney && turret == null)
            meshRend.material.color = hoverColor;
        else
            meshRend.material.color = Color.red;

    }

    private void OnMouseExit()
    {
        meshRend.material.color = startColor;
    }
}
