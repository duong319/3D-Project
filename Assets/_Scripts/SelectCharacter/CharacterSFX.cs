
using UnityEngine;

public class CharacterSFX : MonoBehaviour
{
    public void PlayLeslieSound()
    {
        AudioManager.Instance.Play("G.Leslie");
    }
    public void PlayHaileySound()
    {
        AudioManager.Instance.Play("B.Hailey");
    }
    public void PlayCaitlinSound()
    {
        AudioManager.Instance.Play("R.Caitlin");
    }

    public void PlayCelebrate()
    {
        AudioManager.Instance.Play("LeslieEnd");
    }
}
