using System.Drawing;
using UnityEngine;

public class GameCircle : MonoBehaviour
{
    public float minSize = 30f;
    public float maxSize = 300f;
    public float cycleTime = 1f; // 1√  ¡÷±‚

    private RectTransform rect;
    private float startTime;
    public bool isStop;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    private void OnEnable()
    {
        startTime = Time.unscaledTime;
        SetCurrentScaleMax();
        isStop = false;
    }

    void Update()
    {
        if (isStop)
        {
            return;
        }

        float t = Mathf.PingPong((Time.unscaledTime - startTime) / (cycleTime / 2f), 1f);
        float size = Mathf.Lerp(maxSize, minSize, t);
        rect.sizeDelta = new Vector2(size, size);
    }

    public float GetCurrentSize()
    {
        return rect.sizeDelta.x;
    }

    public void SetCurrentScaleMax()
    {
        rect.sizeDelta = new Vector2(maxSize, maxSize);
    }
}
