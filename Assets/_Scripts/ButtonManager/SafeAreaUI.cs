using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SafeAreaUI : MonoBehaviour
{
    Rect lastSafeArea = new Rect(0, 0, 0, 0);
    RectTransform panel;

    void Awake()
    {
        panel = GetComponent<RectTransform>();
        panel.anchorMin = Vector2.zero;
        panel.anchorMax = Vector2.one;
        panel.offsetMin = Vector2.zero;
        panel.offsetMax = Vector2.zero;

        ApplySafeArea();
    }

    void Update()
    {
        if (lastSafeArea != Screen.safeArea)
            ApplySafeArea();
    }

    void ApplySafeArea()
    {
        Rect safeArea = Screen.safeArea;
        lastSafeArea = safeArea;

        Vector2 anchorMin = new Vector2(safeArea.x / Screen.width, safeArea.y / Screen.height);
        Vector2 anchorMax = new Vector2((safeArea.x + safeArea.width) / Screen.width,
                                        (safeArea.y + safeArea.height) / Screen.height);

        panel.anchorMin = anchorMin;
        panel.anchorMax = anchorMax;
        panel.offsetMin = Vector2.zero;
        panel.offsetMax = Vector2.zero;
    }
}
