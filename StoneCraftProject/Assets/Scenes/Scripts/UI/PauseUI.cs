using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    Canvas PauseCanvas;

    private void Awake()
    {
        PauseCanvas = GetComponent<Canvas>();
        PauseCanvas.enabled = false;
    }

    public void OpenPauseMenu()
    {
        Pause.Instance.OnPause();
        PauseCanvas.enabled = true;
    }

    public void ClosePauseMenu()
    {
        Pause.Instance.OffPause();
        PauseCanvas.enabled = false;
    }

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Escape) && PauseCanvas != null)
    //     {
    //         Debug.Log("ESC KEY");
    // 
    //         if (!isOpenPauseMenu)
    //         { 
    //             Pause.Instance.OnPause();
    //             PauseCanvas.enabled = true;
    //             isOpenPauseMenu = true;
    //         }
    //         else
    //         {
    //             Pause.Instance.OffPause();
    //             PauseCanvas.enabled = false;
    //             isOpenPauseMenu = false;
    //         }
    //     }
    // }
}
