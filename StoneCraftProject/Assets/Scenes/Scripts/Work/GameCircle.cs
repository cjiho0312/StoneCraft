using UnityEngine;

public class GameCircle : MonoBehaviour
{
    public float minScale = 0.3f;
    public float maxScale = 4f;
    public float cycleTime = 1f; // 1√  ¡÷±‚

    private RectTransform rect;
    private float startTime;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    private void OnEnable()
    {
        startTime = Time.unscaledTime;
        SetCurrentScaleMax();
    }

    void Update()
    {
        float t = Mathf.PingPong((Time.unscaledTime - startTime) / (cycleTime / 2f), 1f);
        float scale = Mathf.Lerp(maxScale, minScale, t);
        rect.localScale = new Vector3(scale, scale, 1f);
    }

    public float GetCurrentScale()
    {
        return rect.localScale.x;
    }

    public void SetCurrentScaleMax()
    {
        rect.localScale = new Vector3(maxScale, maxScale, 1f);
    }
}
