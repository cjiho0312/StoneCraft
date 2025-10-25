using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    public static PauseUI Instance;

    Canvas PauseCanvas;
    bool isOpenPauseMenu;
    PlayerState PState;

    List<Animator> pausedAnimators = new();
    public bool GetisOpenPauseMenu {  get { return isOpenPauseMenu; } }

    private void Awake()
    {
        PauseCanvas = GetComponent<Canvas>();
        Instance = this;
        PauseCanvas.enabled = false;
        isOpenPauseMenu = false;
        PState = PlayerState.IDLE;
        Time.timeScale = 1f;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && PauseCanvas != null)
        {
            Debug.Log("ESC KEY");


            if (!isOpenPauseMenu)
            { 
                PState = PlayerManager.Instance.currentState;
                OnPause();
                PlayerManager.Instance.ChangePlayerState(PlayerState.NONE);
            }
            else
            {
                OffPause();
                PlayerManager.Instance.ChangePlayerState(PState);
            }
        }
    }

    public void OnPause()
    {
        PauseCanvas.enabled = true;
        isOpenPauseMenu = true;
        Time.timeScale = 0f;

        PauseAllGameAnimators();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log("Game Paused");
    }

    public void OffPause()
    {
        PauseCanvas.enabled = false;
        isOpenPauseMenu = false;
        Time.timeScale = 1f;

        ResumeAllGameAnimators();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

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
