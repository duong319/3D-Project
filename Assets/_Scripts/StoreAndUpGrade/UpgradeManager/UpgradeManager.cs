using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;
    public List<UpgradeItem> upgrades;

    private const string UpgradeLevelKey = "Upgrade_Level_";
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;         
            LoadUpgrades(); 
        }
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
            PlayerPrefs.SetInt(UpgradeLevelKey + index, item.level);
            PlayerPrefs.Save();
            return true;
        }

        return false;
    }
    private void LoadUpgrades()
    {
        for (int i = 0; i < upgrades.Count; i++)
        {
            int savedLevel = PlayerPrefs.GetInt(UpgradeLevelKey + i, 0);
            upgrades[i].level = savedLevel;
        }
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
