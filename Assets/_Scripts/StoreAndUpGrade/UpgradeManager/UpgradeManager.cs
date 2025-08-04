using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;
    public List<UpgradeItem> upgrades;


    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

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

    public UpgradeItem GetUpgrade(SpecialItemType type)
    {
        return upgrades.Find(u => u.data.itemType == type);
    }

    public float GetDuration(SpecialItemType type)
    {
        var upgrade = GetUpgrade(type);
        return upgrade != null ? upgrade.CurrentDuration : 0f;
    }

    public int GetLevel(SpecialItemType type)
    {
        var upgrade = GetUpgrade(type);
        return upgrade != null ? upgrade.level : 0;
    }


}
