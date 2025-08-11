using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBtn : MonoBehaviour
{

    public void StartBtn()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void Mission()
    {
        SceneManager.LoadScene("Missions");

    }

    public void ShopAndUpgrade()
    {
        SceneManager.LoadScene("Store&&Upgrades");
    }

    public void SelectCharacter()
    {
        SceneManager.LoadScene("Characters");
    }

    public void LeaderBoard()
    {
        SceneManager.LoadScene("LeaderBoard");
    }

    public void Achievement()
    {
        SceneManager.LoadScene("Achievements");
    }

    public void DailyScore()
    {
        SceneManager.LoadScene("DailyScore");
    }

    public void PlayerLevel()
    {
        SceneManager.LoadScene("Level");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }


}
