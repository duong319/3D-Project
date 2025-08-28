using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleUse : MonoBehaviour
{

    public Text HeadStartOwned;
    public Text ScoreBoosterOwned;

    private void Update()
    {
        ShowSingleUse();
    }
    public void ShowSingleUse()
    {
        HeadStartOwned.text = CurrencyManager.Instance.HeadStart.ToString();
        ScoreBoosterOwned.text = CurrencyManager.Instance.ScoreBooster.ToString();
    }

    public void BuyHeadStart()
    {
        if (CurrencyManager.Instance.Coins < 2000) return;
        CurrencyManager.Instance.AddHeadStart(1);
        CurrencyManager.Instance.SpendCoins(2000);
    }

    public void BuyScoreBooster()
    {
        if (CurrencyManager.Instance.Coins < 3000) return;
        CurrencyManager.Instance.AddScoreBooster(1);
        CurrencyManager.Instance.SpendCoins(3000);
    }
}
