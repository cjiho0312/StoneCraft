using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButtons : MonoBehaviour
{
    public void SaveButton()
    {
        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.SavePlayerData();
            Debug.Log("���� ���� �Ϸ�");
        }
    }

    public void LoadButton()
    {
        SaveManager.Instance.LoadPlayerData();
        Debug.Log("���� �ҷ����� �Ϸ�");

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
