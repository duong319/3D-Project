using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBtn : MonoBehaviour
{

    public void StartBtn()
    {
        SceneManager.LoadScene("GamePlay");
        AudioManager.Instance.Play("Btn");
        AudioManager.Instance.Stop("MenuBG");
    }

    public void Mission()
    {
        SceneManager.LoadScene("Missions");
        AudioManager.Instance.Play("Btn");
    }

    public void ShopAndUpgrade()
    {
        SceneManager.LoadScene("Store&&Upgrades");
        AudioManager.Instance.Play("Btn");
    }

    public void SelectCharacter()
    {
        SceneManager.LoadScene("Characters");
        AudioManager.Instance.Play("Btn");
    }

    public void LeaderBoard()
    {
        SceneManager.LoadScene("LeaderBoard");
        AudioManager.Instance.Play("Btn");
    }

    public void Achievement()
    {
        SceneManager.LoadScene("Achievements");
        AudioManager.Instance.Play("Btn");
    }

    public void DailyScore()
    {
        SceneManager.LoadScene("DailyScore");
        AudioManager.Instance.Play("Btn");
    }

    public void PlayerLevel()
    {
        SceneManager.LoadScene("Level");
        AudioManager.Instance.Play("Btn");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        AudioManager.Instance.Play("Close");
        AudioManager.Instance.Play("MenuBG");

    }


}
