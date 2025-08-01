using UnityEngine;

public class UpgradePanel : MonoBehaviour
{
    public UpgradeManager upgradeManager;
    public UpgradeUI upgradeUIPrefab;
    public Transform content;
    public int playerCoins;

    private void Start()
    {
        for (int i = 0; i < upgradeManager.upgrades.Count; i++)
        {
            var ui = Instantiate(upgradeUIPrefab, content);
            int index = i;
            ui.Setup(upgradeManager.upgrades[i], index, TryUpgrade);
        }
    }

    private void TryUpgrade(int index)
    {
        if (upgradeManager.TryUpgrade(index, CurrencyManager.Instance.Coins, out int cost))
        {
            if (CurrencyManager.Instance.Coins < cost) return;
            CurrencyManager.Instance.SpendCoins(cost);
            RefreshUI();
        }
    }

    private void RefreshUI()
    {
        foreach (Transform child in content)
            Destroy(child.gameObject);

        Start();
    }
}
