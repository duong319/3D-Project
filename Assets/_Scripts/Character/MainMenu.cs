using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public Animation anim;

    
    public string[] clipNames = {
        "Event_Girl_alert",
        "Event_Girl_idle1",
        "Event_Girl_idle2",
       
    };

    void Start()
    {
        if (anim == null)
            anim = GetComponent<Animation>();

        StartCoroutine(PlaySequence());
    }

    IEnumerator PlaySequence()
    {
        while (true)
        {
            foreach (string clip in clipNames)
            {
                anim.Play(clip);
                yield return new WaitForSeconds(anim[clip].length);
            }
        }
    }
}
