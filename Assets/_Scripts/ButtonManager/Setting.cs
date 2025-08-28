using System.Collections;


using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public GameObject SettingPanel;
    [SerializeField] private Text playerIdText;
    [SerializeField] private Button enableMusic;
    [SerializeField] private Button disableMusic;

    private void Start()
    {
        enableMusic.onClick.AddListener(OnToggleSound);
        disableMusic.onClick.AddListener(OnToggleSound);
        StartCoroutine(WaitForPlayerId());
    }
    public void SettingEnable()
    {
        AudioManager.Instance.Play("Btn");
        SettingPanel.gameObject.SetActive(true);
    }

    public void SettingDisable()
    {
        AudioManager.Instance.Play("Btn");
        SettingPanel.gameObject.SetActive(false);
    }
    IEnumerator WaitForPlayerId()
    {
        while (string.IsNullOrEmpty(PlayFabLogin.Instance.playerId))
            yield return null;

        playerIdText.text = PlayFabLogin.Instance.playerId;
    }

    public void OnToggleSound()
    {
        var ismute = AudioManager.Instance.IsMuted();
        AudioManager.Instance.ToggleMute();
        enableMusic.gameObject.SetActive(ismute);
        disableMusic.gameObject.SetActive(!ismute);
    }



}
