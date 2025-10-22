using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButtons : MonoBehaviour
{
    public void SaveButton()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SaveGame();
            Debug.Log("���� ���� �Ϸ�");
        }
    }

    public void LoadButton()
    {
        // �� ���ε�
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OptionButton()
    {
        Debug.Log("Option");
    }

    public void MainMenuButton()
    {
        Debug.Log("Go to the Main Menu");
        SceneManager.LoadScene("TitleScene");
    }

}
