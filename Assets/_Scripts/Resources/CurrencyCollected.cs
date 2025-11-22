using UnityEngine;
using System.Collections;

public class CurrencyCollected : MonoBehaviour
{
    public Transform Bag;
    public float StartSpeed ;
    public float MaxSpeed ;
    public float Acceleration ;
    public float ScaleShrinkSpeed;

    private float currentSpeed;

    private Vector3 defaultScale;
    private Vector3 defaultPosition;

    void Awake()
    {
        defaultScale = transform.localScale;
        defaultPosition = transform.position;
    }

    void OnEnable()
    {
        currentSpeed = StartSpeed;
        transform.position = defaultPosition;
        transform.localScale = defaultScale;

        StartCoroutine(MoveToBagRoutine());
    }

    IEnumerator MoveToBagRoutine()
    {
        float timer = 0f;

        while (timer < 2f)
        {
            timer += Time.deltaTime;

            currentSpeed = Mathf.MoveTowards(currentSpeed, MaxSpeed, Acceleration * Time.deltaTime);

            transform.position = Vector3.MoveTowards(transform.position, Bag.position, currentSpeed * Time.deltaTime);

            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, ScaleShrinkSpeed * Time.deltaTime);

            yield return null;
        }
     
    
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }
}
