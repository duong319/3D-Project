

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

    public GameObject VideoChestEnable;
    public GameObject VideoChestDisable;
    public GameObject NormalChestEnable;
    public GameObject NormalChestDisable;
    public GameObject RareChestEnable;
    public GameObject RareChestDisable;

    [Header("Buttons")]
    public Button StoreEnable;
    public Button UpgradeEnable;
    public Button BoxesInfoEnable;
    public Button BoxesInfoDisable;

    public Button VideoChestBtn;
    public Button NormalChestBtn;
    public Button RareChestBtn;


    public void Awake()
    {
        StoreEnable.onClick.AddListener(Store);
        UpgradeEnable.onClick.AddListener(Upgrade);
        BoxesInfoEnable.onClick.AddListener(BoxesInfo);
        BoxesInfoDisable.onClick.AddListener(BoxesInfoClose);
        VideoChestBtn.onClick.AddListener(VideoChest);
        NormalChestBtn.onClick.AddListener(NormalChest);
        RareChestBtn.onClick.AddListener(RareChest);

    }

    public void Store()
    {
        AudioManager.Instance.Play("Btn");
        Debug.Log("Store");
        StorePanel.gameObject.SetActive(true);
        UpgradePanel.gameObject.SetActive(false);
        StoreBtnEnable.gameObject.SetActive(true);
        StoreBtnDisable.gameObject.SetActive(false);
        UpgradeBtnEnable.gameObject.SetActive(false);
        UpgradeBtnDisable.gameObject.SetActive(true);

    }

    public void Upgrade()
    {
        AudioManager.Instance.Play("Btn");
        Debug.Log("Upgrade");
        StorePanel.gameObject.SetActive(false);
        UpgradePanel.gameObject.SetActive(true);
        UpgradeBtnEnable.gameObject.SetActive(true);
        UpgradeBtnDisable.gameObject.SetActive(false);
        StoreBtnEnable.gameObject.SetActive(false);
        StoreBtnDisable.gameObject.SetActive(true);
    }

    public void BoxesInfo()
    {
        AudioManager.Instance.Play("Btn");
        BoxesInfoPanel.gameObject.SetActive(true);
    }

    public void BoxesInfoClose()
    {
        AudioManager.Instance.Play("Close");
        BoxesInfoPanel.gameObject.SetActive(false);
    }

    public void VideoChest()
    {
        AudioManager.Instance.Play("Btn");
        VideoChestEnable.gameObject.SetActive(true);
        VideoChestDisable.gameObject.SetActive(false);

        NormalChestEnable.gameObject.SetActive(false);
        NormalChestDisable.gameObject.SetActive(true);

        RareChestEnable.gameObject.SetActive(false);
        RareChestDisable.gameObject.SetActive(true);
    }

    public void NormalChest()
    {
        AudioManager.Instance.Play("Btn");
        VideoChestEnable.gameObject.SetActive(false);
        VideoChestDisable.gameObject.SetActive(true);

        NormalChestEnable.gameObject.SetActive(true);
        NormalChestDisable.gameObject.SetActive(false);

        RareChestEnable.gameObject.SetActive(false);
        RareChestDisable.gameObject.SetActive(true);
    }

    public void RareChest()
    {
        AudioManager.Instance.Play("Btn");
        VideoChestEnable.gameObject.SetActive(false);
        VideoChestDisable.gameObject.SetActive(true);

        NormalChestEnable.gameObject.SetActive(false);
        NormalChestDisable.gameObject.SetActive(true);

        RareChestEnable.gameObject.SetActive(true);
        RareChestDisable.gameObject.SetActive(false);
    }

}
