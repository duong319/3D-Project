using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class RewardInfoPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI RewardText;
    [SerializeField] private GameObject panel;
    [SerializeField] private Button bgButton;
    [SerializeField] private RectTransform panelRect;

    private static RewardInfoPanel currentOpenPanel = null;

    private Coroutine popupAnim;

    private Vector3 targetScale = new Vector3(2f, 1f, 1f);

    private void Awake()
    {
        if (bgButton == null)
            bgButton = FindFirstObjectByType<BackgroundButton>()?.GetComponent<Button>();
    }

    private void Start()
    {
        bgButton.onClick.AddListener(HideReward);
    }
    public void ShowReward(RewardData reward)
    {
        AudioManager.Instance.Play("Btn");

        if (currentOpenPanel != null && currentOpenPanel != this)
            currentOpenPanel.HideRewardDirect();

        currentOpenPanel = this;
        RewardText.text = $"+{reward.coin.ToString()}";
        panel.SetActive(true);

        if (popupAnim != null) StopCoroutine(popupAnim);
        popupAnim = StartCoroutine(PopupOpen());
    }

    private void HideReward()
    {
        AudioManager.Instance.Play("Btn");
        panel.SetActive(false);
    }

    private void HideRewardDirect()
    {
        panel.SetActive(false);

        if (currentOpenPanel == this)
            currentOpenPanel = null;
    }

    private IEnumerator PopupOpen()
    {
        float time = 0f;
        float duration = 0.25f;

        panelRect.localScale = Vector3.zero;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;


            panelRect.localScale = Vector3.Lerp(Vector3.zero, targetScale, t);

            yield return null;
        }

        panelRect.localScale = targetScale;
    }


}
