using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDead : MonoBehaviour
{
    public GameObject UpgradeNStorePanel;
    public void Resume()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void UpgradeNStore()
    {
        UpgradeNStorePanel.gameObject.SetActive(true);
    }

    public void Close()
    {
        UpgradeNStorePanel.gameObject.SetActive(false);
    }
}
