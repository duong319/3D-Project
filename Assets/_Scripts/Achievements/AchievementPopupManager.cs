using UnityEngine;


public class AchievementPopupManager : MonoBehaviour
{
    public static AchievementPopupManager Instance;

    [SerializeField] private GameObject popupPrefab;
    [SerializeField] private Transform popupParent;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        AchievementManager.Instance.OnAchievementCompleted += ShowPopup;
    }

    private void OnDisable()
    {
        if (AchievementManager.Instance != null)
            AchievementManager.Instance.OnAchievementCompleted -= ShowPopup;
    }

    private void ShowPopup(AchievementData data)
    {
        if (popupParent == null)
        {
            Debug.LogError("Null");
            return;
        }
        GameObject popup = Instantiate(popupPrefab, popupParent);
        popup.GetComponent<AchievementPopupUI>().Setup(data);
    }
}
