using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip OnUI;
    [SerializeField] AudioClip Click;

    public void GameButton()
    {
        audioSource.clip = Click;
        audioSource.Play();
        Debug.Log("Start Button");
        SceneManager.LoadScene("TestScene");
    }

    public void OptionButton()
    {
        audioSource.clip = Click;
        audioSource.Play();
        Debug.Log("Option Button");
    }

    public void ExitButton()
    {
        audioSource.clip = Click;
        audioSource.Play();
        Debug.Log("Exit Button");
        Application.Quit();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        audioSource.clip = OnUI;
        audioSource.Play();
        gameObject.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
