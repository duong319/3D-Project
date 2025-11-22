using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class SpecialItemManager : MonoBehaviour
{
    public static SpecialItemManager Instance;
    public SpecialItemUI specialItemUI;
    [SerializeField] private Button HeadStart;
    [SerializeField] private Button ScoreBooster;
    public bool ScoreBoosterEnabled = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        HeadStart.onClick.RemoveAllListeners();
        HeadStart.onClick.AddListener(UseHeadStart);
        ScoreBooster.onClick.RemoveAllListeners();
        ScoreBooster.onClick.AddListener(UseScoreBooster);
        StartCoroutine(DisableItem());
    }

    public void UseItem(SpecialItemType itemType)
    {
        int level = UpgradeManager.Instance.GetLevel(itemType);
        float duration = UpgradeManager.Instance.GetDuration(itemType);
        Sprite icon = UpgradeManager.Instance.GetUpgrade(itemType).data.icon;
        specialItemUI.Activate(icon, duration);
        if (level == 0)
        {
            duration = 7f;
            specialItemUI.Activate(icon, duration);
        }
        Debug.Log(duration);
        switch (itemType)
        {
            case SpecialItemType.Shield:
                StartCoroutine(ActivateShield(duration));
                break;
            case SpecialItemType.Magnet:
                StartCoroutine(ActivateMagnet(duration));
                break;
            case SpecialItemType.Headstart:
                StartCoroutine(ActivateHeadstart(duration));
                break;
            case SpecialItemType.ScoreMultiplier:
                StartCoroutine(ActivateScoreMultiplier(duration));
                break;

        }
    }

    public void UseHeadStart()
    {
        if (CurrencyManager.Instance.HeadStart <= 0) return;
        CurrencyManager.Instance.SpendHeadStart(1);
        UseItem(SpecialItemType.Headstart);
        HeadStart.gameObject.SetActive(false);
    }

    public void UseScoreBooster()
    {
        if (CurrencyManager.Instance.ScoreBooster <= 0) return;
        CurrencyManager.Instance.SpendScoreBooster(1);
        ScoreBoosterEnabled = true;
        int level = UpgradeManager.Instance.GetLevel(SpecialItemType.ScoreBooster);
        float duration = UpgradeManager.Instance.GetDuration(SpecialItemType.ScoreBooster);
        if (level == 0)
        {
            duration = 6f;

        }
        PlayerController.Instance.AddMultiplier(((int)duration - 1));
        UIManager.Instance.UpdateScoreMultiplier();
        ScoreBooster.gameObject.SetActive(false);
    }

    public void EndScoreBooster()
    {
        int level = UpgradeManager.Instance.GetLevel(SpecialItemType.ScoreBooster);
        float duration = UpgradeManager.Instance.GetDuration(SpecialItemType.ScoreBooster);
        if (level == 0)
        {
            duration = 6f;
        }
        PlayerController.Instance.ResetMultiplier(((int)duration - 1));
        UIManager.Instance.UpdateScoreMultiplier();
        ScoreBoosterEnabled = false;
    }

    #region Item Effects

    IEnumerator ActivateShield(float duration)
    {
        AudioManager.Instance.Play("Shield");
        PlayerController.Instance.SetShield(true);
        yield return new WaitForSeconds(duration);
        if (PlayerController.Instance.isShieldAvtivate == true)
        {
            PlayerController.Instance.SetShield(false);
        }
    }

    IEnumerator ActivateMagnet(float duration)
    {
        AudioManager.Instance.Play("Magnet");
        PlayerController.Instance.SetMagnet(true);
        yield return new WaitForSeconds(duration);
        PlayerController.Instance.SetMagnet(false);
        AudioManager.Instance.Stop("Magnet");
        AudioManager.Instance.Play("MagnetEnd");
    }

    IEnumerator ActivateHeadstart(float duration)
    {
        PlayerController.Instance.ActivateHeadstart();
        yield return new WaitForSeconds(duration);
        PlayerController.Instance.EndHeadstart();
    }

    IEnumerator ActivateScoreMultiplier(float duration)
    {
        AudioManager.Instance.Play("X2");
        PlayerController.Instance.SetMultiplier(2);
        UIManager.Instance.UpdateScoreMultiplier();
        yield return new WaitForSeconds(duration);
        PlayerController.Instance.EndMultiplier(2);
        UIManager.Instance.UpdateScoreMultiplier();
        AudioManager.Instance.Stop("X2");
        AudioManager.Instance.Play("X2End");
    }

    IEnumerator DisableItem()
    {
        yield return new WaitForSeconds(5f);
        HeadStart.gameObject.SetActive(false);
        ScoreBooster.gameObject.SetActive(false);
    }

    #endregion
}
