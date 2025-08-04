using UnityEngine;
using System.Collections;

public class SpecialItemManager : MonoBehaviour
{
    public static SpecialItemManager Instance;
    public SpecialItemUI specialItemUI;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
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
            case SpecialItemType.ScoreBooster:
                PlayerController.Instance.AddMultiplier(((int)duration-1)); 
                break;
        }
    }

    #region Item Effects

    IEnumerator ActivateShield(float duration)
    {
        Debug.Log("Shield");
        PlayerController.Instance.SetShield(true);
        yield return new WaitForSeconds(duration);
        PlayerController.Instance.SetShield(false);
    }

    IEnumerator ActivateMagnet(float duration)
    {
        Debug.Log("magnet");
        PlayerController.Instance.SetMagnet(true);
        yield return new WaitForSeconds(duration);
        PlayerController.Instance.SetMagnet(false);
    }

    IEnumerator ActivateHeadstart(float duration)
    {
        Debug.Log("headStart");
        PlayerController.Instance.ActivateHeadstart(); 
        yield return new WaitForSeconds(duration);
        PlayerController.Instance.EndHeadstart(); 
    }

    IEnumerator ActivateScoreMultiplier(float duration)
    {
        PlayerController.Instance.SetMultiplier(2);
        UIManager.Instance.UpdateScoreMultiplier();
        yield return new WaitForSeconds(duration);
        PlayerController.Instance.EndMultiplier(2);
        UIManager.Instance.UpdateScoreMultiplier();
    }

    #endregion
}
