using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public GameObject SettingPanel;
    [SerializeField] private Text playerIdText;

    private void Start()
    {
       
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


}
