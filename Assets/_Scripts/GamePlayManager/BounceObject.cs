using UnityEngine;

public class BounceObject : MonoBehaviour
{
    public float upwardForce ;   
    public float forwardForce ;   
  

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
        
            PlayerBounce pb = other.GetComponent<PlayerBounce>();
            if (pb != null)
            {
                pb.Bounce(upwardForce, forwardForce, transform.forward);
            }
            AudioManager.Instance.Play("KnockBack");      
        }
    }
}
