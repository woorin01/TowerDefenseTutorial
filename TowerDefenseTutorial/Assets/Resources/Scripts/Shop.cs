using UnityEngine;

public class Shop : MonoBehaviour
{
    public TowerBlueprint standardTurret;
    public TowerBlueprint missileLauncher;
    public TowerBlueprint laserBeamer;

    public void SelectStandardTurret()
    {
        BuildManager.instance.SelectTurretToBuild(standardTurret);
    }

    public void SelectMissileLauncher()
    {
        BuildManager.instance.SelectTurretToBuild(missileLauncher);
    }

    public void SelectLaserBeamer()
    {
        BuildManager.instance.SelectTurretToBuild(laserBeamer);
    }
}
