using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using TMPro;

public class ChangeNameUI : MonoBehaviour
{
    [SerializeField] private InputField nameInput;
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI currentNameText;

    private const string PlayerNameKey = "PlayerName";

    private void Start()
    {
        if (PlayerPrefs.HasKey(PlayerNameKey))
        {
            string savedName = PlayerPrefs.GetString(PlayerNameKey);
            currentNameText.text = savedName;
        }
        else
        {
            string guestName = "Guest" + Random.Range(1, 100);
            currentNameText.text = guestName;

            PlayerPrefs.SetString(PlayerNameKey, guestName);
            PlayerPrefs.Save();

            var request = new UpdateUserTitleDisplayNameRequest
            {
                DisplayName = guestName
            };

            PlayFabClientAPI.UpdateUserTitleDisplayName(request,
                result => {  },
                error => {  }
            );
        }
    }

    public void OpenPanel()
    {
        AudioManager.Instance.Play("Btn");
        panel.SetActive(true);
        nameInput.text = PlayerPrefs.GetString(PlayerNameKey, "");
    }

    public void ClosePanel()
    {
        AudioManager.Instance.Play("Close");
        panel.SetActive(false);
    }

    public void OnSaveClicked()
    {
        AudioManager.Instance.Play("Btn");
        string newName = nameInput.text.Trim();

        if (string.IsNullOrEmpty(newName))
        {
            return;
        }

        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = newName
        };

        PlayFabClientAPI.UpdateUserTitleDisplayName(request,
            result =>
            {    
                currentNameText.text = result.DisplayName;
                PlayerPrefs.SetString(PlayerNameKey, result.DisplayName);
                PlayerPrefs.Save();
                ClosePanel();
            },
            error =>
            {
               
            });
    }
}
