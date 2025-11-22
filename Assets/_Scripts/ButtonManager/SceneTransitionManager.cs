using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public GameObject MenuUI;
    public GameObject menuUI;
    public GameObject MenuBg;
    public GameObject GamePlayBG;
    public GameObject TransitionFX;

    public void GameStart()
    {
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        AudioManager.Instance.Play("Btn");
        AudioManager.Instance.Stop("MenuBG");
        MenuUI.gameObject.SetActive(false);
        menuUI.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        TransitionFX.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.6f);
        MenuBg.gameObject.SetActive(false);
        GamePlayBG.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("GamePlay");
    }

}
