using UnityEngine;

public class Effect : MonoBehaviour
{
    public ParticleSystem effect;

    private void Awake()
    {
        effect.Play();
    }

}
