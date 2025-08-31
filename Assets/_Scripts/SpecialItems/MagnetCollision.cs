
using UnityEngine;

public class MagnetCollision : MonoBehaviour
{
    public float attractSpeed = 10f;
    public Transform player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin") && PlayerController.Instance.isMagnetAvtivate == true)
        {
            
            CoinMovement coin = other.GetComponent<CoinMovement>();
            if (coin == null) Debug.Log("null"); 
            if (coin != null)
            {   
                coin.AttractTo(player, attractSpeed);
            }
        }
    }
}
