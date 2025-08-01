using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public List<UpgradeItem> upgrades;

    public bool TryUpgrade(int index, int currentCoins, out int cost)
    {
        cost = 0;
        if (index >= upgrades.Count) return false;

        var item = upgrades[index];
        if (!item.CanUpgrade) return false;

        cost = item.CurrentPrice;
        if (currentCoins >= cost)
        {
            item.Upgrade();
            return true;
        }

        return false;
    }
}
