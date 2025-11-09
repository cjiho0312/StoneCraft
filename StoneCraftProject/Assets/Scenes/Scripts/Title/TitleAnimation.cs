using UnityEngine;

public class TitleAnimation : MonoBehaviour
{
    [SerializeField] GameObject Start;
    [SerializeField] GameObject Option;
    [SerializeField] GameObject Exit;

    bool isFinished;
    RectTransform RectT;
    float yy = 873;

    public void Awake()
    {
        Start.SetActive(false);
        Option.SetActive(false);
        Exit.SetActive(false);

        RectT = GetComponent<RectTransform>();
        RectT.anchoredPosition = new Vector2(0, yy);
        isFinished = false;
        yy = 873;
    }

    void Update()
    {
        if (isFinished) return;


        RectT.anchoredPosition = new Vector2(0, yy);

        yy -= 5;

        if (RectT.anchoredPosition.y <= 256)
        {
            isFinished = true;
        }

        if(isFinished)
        {
            Start.SetActive(true);
            Option.SetActive(true);
            Exit.SetActive(true);
        }
    }
}
