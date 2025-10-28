using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    public static PauseUI Instance;

    Canvas PauseCanvas;
    bool isOpenPauseMenu;

    public bool GetisOpenPauseMenu {  get { return isOpenPauseMenu; } }

    private void Awake()
    {
        PauseCanvas = GetComponent<Canvas>();
        Instance = this;
        PauseCanvas.enabled = false;
        isOpenPauseMenu = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && PauseCanvas != null)
        {
            Debug.Log("ESC KEY");

            if (!isOpenPauseMenu)
            { 
                Pause.Instance.OnPause();
                PauseCanvas.enabled = true;
                isOpenPauseMenu = true;
            }
            else
            {
                Pause.Instance.OffPause();
                PauseCanvas.enabled = false;
                isOpenPauseMenu = false;
            }
        }
    }
}
