using UnityEngine;

public class Shop : MonoBehaviour
{
    public TowerBlueprint standardTurret;
    public TowerBlueprint missileLaundcher;

    public void SelectStandardTurret()
    {
        BuildManager.instance.SelectTurretToBuild(standardTurret);
    }

    public void SelectMissileLauncher()
    {
        BuildManager.instance.SelectTurretToBuild(missileLaundcher);
    }

}
