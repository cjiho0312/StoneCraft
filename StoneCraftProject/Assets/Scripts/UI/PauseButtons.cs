using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButtons : MonoBehaviour
{
    public void SaveButton()
    {
        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.SavePlayerData();
            Debug.Log("게임 저장 완료");
        }
    }

    public void LoadButton()
    {
        SaveManager.Instance.LoadPlayerData();
        Debug.Log("게임 불러오기 완료");

        // 씬 리로드
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
