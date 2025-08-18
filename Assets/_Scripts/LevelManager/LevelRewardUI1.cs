using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelRewardUI1 : MonoBehaviour
{
    public Slider ProgressBar;
   
    public List<LevelRewardTierUI> LevelrewardTierUIs;
    public float totalProgress = 700f;

    private void Awake()
    {
        UpdateUI();
        Debug.Log(ProgressBar.value);
        Debug.Log(totalProgress);
        Debug.Log(CurrencyManager.Instance.totalExp);
    }

    public void UpdateUI()
    {
        // ProgressBar.value = Mathf.Clamp01((float)CurrencyManager.Instance.totalExp / totalProgress);
        ProgressBar.minValue = 0;
        ProgressBar.maxValue = totalProgress;
        ProgressBar.value = CurrencyManager.Instance.totalExp;  

        int currentExp = CurrencyManager.Instance.totalExp;
       

        foreach (var tierUI in LevelrewardTierUIs)
        {
            tierUI.UpdateTier(currentExp);
        }
    }
}
