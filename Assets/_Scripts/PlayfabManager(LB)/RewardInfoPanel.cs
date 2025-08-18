using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class RewardInfoPanel : MonoBehaviour
{
    
    public Text rewardText;
    public GameObject panel;

    public void ShowReward(RewardData reward)
    {
       
        rewardText.text = reward.coin.ToString();

        panel.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(HideRewardCoroutine());
    }

    private IEnumerator HideRewardCoroutine()
    {
        yield return new WaitForSeconds(2f);
        panel.SetActive(false);
    }
}
