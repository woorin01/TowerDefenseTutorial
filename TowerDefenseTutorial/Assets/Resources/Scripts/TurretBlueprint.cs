using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretBlueprint
{
    public GameObject prefab;
    public GameObject upgradePrefab;

    public int cost;
    public int upgradeCost;

    public int GetSellAmount() => cost / 2;
    public int GetSellAmountIsUpgraded() => (cost + upgradeCost) / 2;
}
