using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButtons : MonoBehaviour
{
    public void SaveButton()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SaveGame();
            Debug.Log("게임 저장 완료");
        }
        AudioManager.Instance.PlayClick1Sound();
    }

    public void LoadButton()
    {
        AudioManager.Instance.PlayClick1Sound();

        // 씬 리로드
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OptionButton()
    {
        Debug.Log("Option");
        AudioManager.Instance.PlayClick1Sound();
    }

    public void MainMenuButton()
    {
        AudioManager.Instance.PlayClick1Sound();
        SceneManager.LoadScene("TitleScene");
    }

}
