using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButtons : MonoBehaviour
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

}
