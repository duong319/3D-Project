using UnityEngine;

[ExecuteAlways] 
public class BackgroundScaler : MonoBehaviour
{
    void LateUpdate()
    {
        Camera cam = Camera.main;
        if (cam == null) return;
    
        float distance = Mathf.Abs(transform.position.z - cam.transform.position.z);
    
        float frustumHeight = 2f * distance * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
      
        float frustumWidth = frustumHeight * cam.aspect;
      
        transform.localScale = new Vector3(frustumWidth, frustumHeight, 1f);
    }
}
