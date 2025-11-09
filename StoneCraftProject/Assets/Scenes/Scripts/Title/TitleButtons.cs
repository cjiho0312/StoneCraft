using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void GameButton()
    {
        Debug.Log("Start Button");
        SceneManager.LoadScene("TestScene");
    }

    public void OptionButton()
    {
        Debug.Log("Option Button");
    }

    public void ExitButton()
    {
        Debug.Log("Exit Button");
        Application.Quit();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
