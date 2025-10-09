using TMPro;
using UnityEngine;

public class MedalsProgressUI : MonoBehaviour
{
    public TextMeshProUGUI currentMedal;
    public TextMeshProUGUI totalMedal;
    public AchievementData data;
    public int currentmedal;


    private void Start()
    {
        currentmedal = CurrencyManager.Instance.AchievementMedals;
        totalMedal.text = $"/{data.totalmedal.ToString()}";
        currentMedal.text = currentmedal.ToString();
    }
}
