using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint standardTurret;
    public TurretBlueprint missileLauncher;
    public TurretBlueprint laserBeamer;

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
