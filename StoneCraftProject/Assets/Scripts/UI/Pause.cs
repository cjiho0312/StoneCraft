using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static Pause Instance;
    bool isPause;
    PlayerState PState;
    List<Animator> pausedAnimators = new();

    private void Awake()
    {
        Instance = this;
        isPause = false;
        PState = PlayerState.IDLE;
        Time.timeScale = 1f;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OnPause()
    {
        if (isPause) { return; }

        PState = PlayerManager.Instance.currentState;

        isPause = true;
        Time.timeScale = 0f;

        PauseAllGameAnimators();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log("Game Paused");

        PlayerManager.Instance.ChangePlayerState(PlayerState.NONE);
    }

    public void OffPause()
    {
        if (!isPause) { return; }

        isPause = false;
        Time.timeScale = 1f;

        ResumeAllGameAnimators();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        PlayerManager.Instance.ChangePlayerState(PState);

        Debug.Log("Game Resumed");
    }

    // 게임 내 애니메이터 정지 (UI는 제외)
    private void PauseAllGameAnimators()
    {
        pausedAnimators.Clear();

        Animator[] allAnimators = Object.FindObjectsByType<Animator>(FindObjectsSortMode.None);

        foreach (var animator in allAnimators)
        {
            if (animator == null)
                continue;
            if (animator.CompareTag("UIAnim"))
                continue;

            animator.speed = 0f;
            pausedAnimators.Add(animator);
        }
    }

    // 다시 재생
    private void ResumeAllGameAnimators()
    {
        foreach (var animator in pausedAnimators)
        {
            if (animator != null)
                animator.speed = 1f;
        }
        pausedAnimators.Clear();
    }
}
