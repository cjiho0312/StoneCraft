using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum GuideSub
{
    GETITEM,
    GUIDE
}


public class GuideTextManager : MonoBehaviour
{
    public static GuideTextManager Instance;

    [SerializeField] GameObject Guide;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
    }

    public void MakeGuide(GuideSub s, string t)
    {
        GameObject newGuide = Instantiate(Guide);
        newGuide.transform.SetParent(gameObject.transform, false);

        Text text = newGuide.GetComponentInChildren<Text>();

        switch(s)
        {
            case GuideSub.GETITEM:
                text.color = Color.white;
                break;
            case GuideSub.GUIDE:
                text.color = Color.yellow;
                break;
        }

        text.text = t;
        StartCoroutine(DisplayText(newGuide));
    }

    IEnumerator DisplayText(GameObject guide)
    {
        guide.SetActive(true);

        Image image = guide.GetComponent<Image>();

        float f = 0f;

        while (f < 0.3f)
        {
            f += 0.1f;
            Color ColorAlhpa = image.color;
            ColorAlhpa.a = f;
            image.color = ColorAlhpa;
            yield return new WaitForSecondsRealtime(0.02f);
        }

        yield return new WaitForSecondsRealtime(1.5f);


        while (f > 0.01)
        {
            f -= 0.1f;
            Color ColorAlhpa = image.color;
            ColorAlhpa.a = f;
            image.color = ColorAlhpa;
            yield return new WaitForSecondsRealtime(0.05f);
        }

        Destroy(guide);
    }
}
