
using UnityEngine;


public class CoinMovement : MonoBehaviour
{
    private Transform player;
    private float magnetSpeed = 500f;
    private bool isBeingAttracted = false;
    private float rotationSpeed = 200f;

    public void AttractTo(Transform target, float speed)
    {    
        player = target;
        magnetSpeed = speed;

        isBeingAttracted = true;
    }

    private void Update()
    {
        if (isBeingAttracted && player != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, magnetSpeed * Time.deltaTime);
        }
       transform.Rotate(0,0,rotationSpeed*Time.deltaTime);
    }
}
