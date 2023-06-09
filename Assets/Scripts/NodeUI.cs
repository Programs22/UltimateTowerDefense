using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    public GameObject canvas;
    public Text upgradeCost;
    public Button upgradeButton;
    public Text sellAmount;

    private Node target;

    public void SetTarget(Node node)
    {
        target = node;
        transform.position = target.GetBuildPosition();

        if (!target.isUpgraded)
        {
            upgradeCost.text = "$" + target.turretBlueprint.upgradeCost;
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeCost.text = "Maxed out";
            upgradeButton.interactable = false;
        }

        sellAmount.text = "$" + target.SellCost();

        canvas.SetActive(true);
    }

    public void Hide()
    {
        canvas.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }
}
