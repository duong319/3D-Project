using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPManager : MonoBehaviour
{

    public static IAPManager Instance;
    [SerializeField] private GameObject PurchaseFailed;
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void OnCoinPurchase(int amount)
    {
        CurrencyManager.Instance.AddCoins(amount);
    }

    public void OnGemPurchase(int amount)
    {
        CurrencyManager.Instance.AddGems(amount);
    }

    public void OnPurchaseFailed()
    {
       StartCoroutine(purchaseFailedPanel());   
    }

    private IEnumerator purchaseFailedPanel()
    {
        PurchaseFailed.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        PurchaseFailed.gameObject.SetActive(false);
    }

}
