using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    public GameObject canvas;
    public Text upgradeCost;
    public Button upgradeButton;

    public Text sellAmount;

    private Node target;

    public void SetTarget(Node _target)
    {
        target = _target;

        transform.position = _target.GetBuildPosition();

        if (!_target.isUpgraded)
        {
            upgradeButton.interactable = true;
            upgradeCost.text = "$" + _target.turretBlueprint.upgradeCost.ToString();
            sellAmount.text = "$" + _target.turretBlueprint.GetSellAmount().ToString();
        }
        else
        {
            upgradeButton.interactable = false;
            upgradeCost.text = "DONE";
            sellAmount.text = "$" + _target.turretBlueprint.GetSellAmountIsUpgraded().ToString();
        }
       

        canvas.SetActive(true);
    }

    public void Hide()
    {
        canvas.SetActive(false);
    }

    public void TurretUpgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }

    public void TurretSell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }
}
