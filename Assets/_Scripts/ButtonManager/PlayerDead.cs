
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDead : MonoBehaviour
{
    public GameObject UpgradeNStorePanel;
    public void Resume()
    {
        AudioManager.Instance.Play("Btn");
        SceneManager.LoadScene("GamePlay");
    }

    public void MainMenu()
    {
        AudioManager.Instance.Play("Btn");
        AudioManager.Instance.Stop("GamePlayBG");
        AudioManager.Instance.Play("MenuBG");
        SceneManager.LoadScene("MainMenu");
    }

    public void UpgradeNStore()
    {
        AudioManager.Instance.Play("Btn");
        UpgradeNStorePanel.gameObject.SetActive(true);
    }

    public void Close()
    {
        AudioManager.Instance.Play("Close");
        UpgradeNStorePanel.gameObject.SetActive(false);
    }
}
