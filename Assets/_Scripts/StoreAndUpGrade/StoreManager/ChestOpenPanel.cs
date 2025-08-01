using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestOpenPanel : MonoBehaviour
{
    public GameObject panel;
   // public Animator chestAnimator;
    public Transform rewardParent;
    public GameObject rewardItemPrefab;
    public Button continueBtn;
    public Image chestIconImage;
    public float delayBetweenRewards = 0.6f;

    private List<GameObject> currentRewards = new();

    public void ShowChestOpenAnimation(List<Reward> rewards, ChestData chestData)
    {
        chestIconImage.sprite = chestData.chestIcon;
        panel.SetActive(true);
        ClearRewards();
      //  chestAnimator.Play("Open");
        

        StartCoroutine(ShowRewardSequence(rewards));
    }

    IEnumerator ShowRewardSequence(List<Reward> rewards)
    {
        yield return new WaitForSeconds(1f); 

        foreach (var reward in rewards)
        {
            GameObject go = Instantiate(rewardItemPrefab, rewardParent);
            RewardItemUI ui = go.GetComponent<RewardItemUI>();
            int amount = reward.GetRandomAmount();
            ui.Setup(reward.icon, reward.name, amount);
            currentRewards.Add(go);

            yield return new WaitForSeconds(delayBetweenRewards);
        }

        continueBtn.interactable = true;
    }

    public void OnClickSkip()
    {
        StopAllCoroutines();
        
    }

    public void OnClickContinue()
    {
        panel.SetActive(false);
        ClearRewards();
    }

    void ClearRewards()
    {
        foreach (var go in currentRewards)
            Destroy(go);

        currentRewards.Clear();
    }
}
