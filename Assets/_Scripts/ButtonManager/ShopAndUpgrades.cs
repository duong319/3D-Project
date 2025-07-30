using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.UI;

public class ShopAndUpgrades : MonoBehaviour
{
    [Header("GameObject")]
    public GameObject StorePanel;
    public GameObject UpgradePanel;
    public GameObject StoreBtnEnable;
    public GameObject StoreBtnDisable;
    public GameObject UpgradeBtnEnable;
    public GameObject UpgradeBtnDisable;
    public GameObject BoxesInfoPanel;

    [Header("Buttons")]
    public Button StoreEnable;
    public Button StoreDisable;
    public Button UpgradeEnable;
    public Button UpgradeDisable;
    public Button BoxesInfoEnable;
    public Button BoxesInfoDisable;


    public void Awake()
    {
        StoreEnable.onClick.AddListener(Store);
        UpgradeEnable.onClick.AddListener(Upgrade);

    }

    public void Store()
    {
        Debug.Log("Store");
        StorePanel.SetActive(true);
        UpgradePanel.SetActive(false);
        StoreBtnEnable.SetActive(true);
        StoreBtnDisable.SetActive(false);
        UpgradeBtnEnable.SetActive(false);
        UpgradeBtnDisable.SetActive(true);
    }

    public void Upgrade()
    {
        Debug.Log("Upgrade");
        StorePanel.SetActive(false);
        UpgradePanel.SetActive(true);
        UpgradeBtnEnable.SetActive(true);
        UpgradeBtnDisable.SetActive(false);
        StoreBtnEnable.SetActive(false);
        StoreBtnDisable.SetActive(true);
    }


}
