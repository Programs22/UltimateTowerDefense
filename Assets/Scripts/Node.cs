using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Vector3 positionOffset;
    [HideInInspector]
    public GameObject turret;
    public Color errorColor;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    private Renderer rendererObject;
    private Color startColor;
    
    BuildManager buildManager;

    void Start()
    {
        rendererObject = GetComponent<Renderer>();
        startColor = rendererObject.material.color;
        buildManager = BuildManager.instance;
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild())
            return;

        BuildTurret(buildManager.GetTurretBlueprint());
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild())
            return;

        if (buildManager.HasMoney())
            rendererObject.material.color = hoverColor;
        else
            rendererObject.material.color = errorColor;    
    }

    void OnMouseExit()
    {
        rendererObject.material.color = startColor;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    void BuildTurret(TurretBlueprint blueprint)
    {
        if (PlayerStats.money < blueprint.cost)
            return;

        PlayerStats.money -= blueprint.cost;
            
        GameObject mTurret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = mTurret;

        turretBlueprint = blueprint;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
    }

    public void UpgradeTurret()
    {
        if (PlayerStats.money < turretBlueprint.upgradeCost)
            return;

        PlayerStats.money -= turretBlueprint.upgradeCost;

        Destroy(turret);
            
        GameObject mTurret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = mTurret;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        isUpgraded = true;
    }

    public int SellCost()
    {
        if (!isUpgraded)
            return (int)(turretBlueprint.cost * turretBlueprint.sellPercentage);
        else
            return (int)(turretBlueprint.upgradeCost * turretBlueprint.sellPercentage);
    }

    public void SellTurret()
    {
        PlayerStats.money += SellCost();

        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        Destroy(turret);
        turretBlueprint = null;
        isUpgraded = false;        
    }
}
