using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint turret;
    public TurretBlueprint missileLauncher;
    public TurretBlueprint laserBeamer;
    
    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectStandardTurret()
    {
        Debug.Log("Standard Turret selected");
        buildManager.SelectTurretToBuild(turret);
    }

    public void SelectMissileLauncher()
    {
        Debug.Log("Missile Launcher selected");
        buildManager.SelectTurretToBuild(missileLauncher);
    }

    public void SelectLaserBeamer()
    {
        Debug.Log("Laser Beamer selected");
        buildManager.SelectTurretToBuild(laserBeamer);
    }
}
