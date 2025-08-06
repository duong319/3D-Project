using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelRewardUI1 : MonoBehaviour
{
    public Slider ProgressBar;
   
    public List<LevelRewardTierUI> LevelrewardTierUIs;
    public float totalProgress = 700f;

    private void OnEnable()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        ProgressBar.value = Mathf.Clamp01((float)CurrencyManager.Instance.Exp / totalProgress);
       
        int currentExp = CurrencyManager.Instance.Exp;
       



        foreach (var tierUI in LevelrewardTierUIs)
        {
            tierUI.UpdateTier(currentExp);
        }
    }
}
