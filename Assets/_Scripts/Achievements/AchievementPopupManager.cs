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

    private void Start()
    {
        if (AchievementManager.Instance != null)
            AchievementManager.Instance.OnAchievementCompleted += ShowPopup;
    }

    private void OnDisable()
    {
        if (AchievementManager.Instance != null)
            AchievementManager.Instance.OnAchievementCompleted -= ShowPopup;
    }

    private void ShowPopup(AchievementData data)
    {
        if (popupParent == null || popupPrefab == null || data == null)
        {
            return;
        }
        GameObject popup = Instantiate(popupPrefab, popupParent);
        popup.GetComponent<AchievementPopupUI>().Setup(data);
    }
}
