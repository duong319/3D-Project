using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeadUI : MonoBehaviour
{
    public Text Score;
    public Text Coin;
    public GameObject RankPanel;
    public GameObject char1;
    public GameObject char2;
    Animator Animator;
    [SerializeField] private Animator characterAnimator;

    private void Awake()
    {
        Score.text = ScoreManager.Instance.lastScore.ToString();
        Coin.text = ScoreManager.Instance.currentCoins.ToString();
        Animator = GetComponent<Animator>();
    }

    public void WatchAd()
    {
        //AD
    }

    public void Continue()
    {
        Animator.SetTrigger("Continue");
        characterAnimator.SetBool("Continue", true);
        RankPanel.gameObject.SetActive(true);
        StartCoroutine(CharActivate());

    }

    IEnumerator CharActivate()
    {
        yield return new WaitForSeconds(0.5f);
        char1.gameObject.SetActive(false);
        char2.gameObject.SetActive(true) ;
    }
}
