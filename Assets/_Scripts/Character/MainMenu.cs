using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public Animation anim;

    
    public string[] clipNames = {
        "Runner_G01_alert",
        "Runner_G01_idle 1",
        "Runner_G01_idle 2",
       
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
