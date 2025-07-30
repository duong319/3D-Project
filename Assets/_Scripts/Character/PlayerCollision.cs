using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Obstacle"))
        {
             
            PlayerController player = GetComponent<PlayerController>();
            if (player == null)
            {
                player = GetComponentInParent<PlayerController>();
            }

            if (player != null)
            {
                player.Die();
            }
            else
            {
                Debug.LogWarning("PlayerController!");
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Coin"))
        {
           
            ScoreManager.Instance.AddCoin(1);
            Destroy(other.gameObject);
            MissionManager.Instance.ReportProgress(MissionType.CollectCoin, 1);
            CurrencyManager.Instance.AddCoins(1);

        }

        if (other.CompareTag("PowerUp"))
        {
            //Debug.Log("PowerUp!");
            Destroy(other.gameObject);

        }
    }
}
