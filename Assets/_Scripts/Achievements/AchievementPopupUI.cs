using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AchievementPopupUI : MonoBehaviour
{
    public Image icon;
    public Text titleText;   
    private float showTime = 2.5f;

    public void Setup(AchievementData data)
    {
        icon.sprite = data.icon;
        titleText.text = data.title;    
        StartCoroutine(AutoHide());
    }

    private IEnumerator AutoHide()
    {
        yield return new WaitForSeconds(showTime);
        Destroy(gameObject);
    }
}
