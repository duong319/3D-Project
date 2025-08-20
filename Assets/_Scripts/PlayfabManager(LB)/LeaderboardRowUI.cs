using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LeaderboardRowUI : MonoBehaviour
{
    public Text rankText;
    public Text nameText;
    public Text scoreText;
    public Image countryFlagImage;
    public Image RankIcon;
    public Image rewardIconImage;
    private RewardData rewardData;
    [SerializeField] private RewardInfoPanel rewardInfoPanel;



    public void SetData(int rank, string name, int score, string countryCode)
    {
        rankText.text = rank.ToString();
        nameText.text = string.IsNullOrEmpty(name) ? "Guest" : name;
        scoreText.text = score.ToString();


        Sprite flag = CountryFlagManager.Instance.GetFlagSprite(countryCode);
        Sprite rankicon = RankIconManager.Instance.GetRankSprite(rank);

        if (flag != null)
        {
            countryFlagImage.sprite = flag;
            countryFlagImage.gameObject.SetActive(true);
        }
        else
        {
            countryFlagImage.gameObject.SetActive(false);
        }

        if (rankicon != null)
        {
            RankIcon.sprite = rankicon;
            RankIcon.gameObject.SetActive(true);
        }
        else
        {
            RankIcon.gameObject.SetActive(false);
        }

        rewardData = RewardManager.Instance.GetReward(rank);
        if (rewardData != null)
        {
            rewardIconImage.sprite = rewardData.rewardIcon;
            rewardIconImage.gameObject.SetActive(true);


            rewardIconImage.GetComponent<Button>().onClick.AddListener(() =>
            {

                rewardInfoPanel.ShowReward(rewardData);

            });

        }

    }
}
