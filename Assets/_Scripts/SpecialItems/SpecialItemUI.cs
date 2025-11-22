using UnityEngine;
using UnityEngine.UI;

public class SpecialItemUI : MonoBehaviour
{
    public Image iconImage;
    public Image durationBar;


    private float duration;
    private float timer;
    private bool isActive;


    public void Activate(Sprite icon, float itemDuration)
    {    
        iconImage.sprite = icon;
        duration = itemDuration;
        timer = duration;
        isActive = true;
        gameObject.SetActive(true);
    }
    public void OnDestroy()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!isActive) return;

        timer -= Time.deltaTime;
        durationBar.fillAmount = timer / duration;

        if (timer <= 0f)
        {
            isActive = false;
            gameObject.SetActive(false);
        }
    }
}
