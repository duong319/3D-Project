using UnityEngine;

public class CurrencyCollected : MonoBehaviour
{
    public Transform Bag;
    public float StartSpeed ;
    public float MaxSpeed ;
    public float Acceleration ;
    public float ScaleShrinkSpeed ;

    private float currentSpeed;

    void Start()
    {
        currentSpeed = StartSpeed;
    }

    void Update()
    {
        currentSpeed = Mathf.MoveTowards(currentSpeed, MaxSpeed, Acceleration * Time.deltaTime);


        transform.position = Vector3.MoveTowards(transform.position,Bag.position,currentSpeed * Time.deltaTime);


        transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero,ScaleShrinkSpeed * Time.deltaTime);
    }
}
