using System;
using UnityEngine;

public class SculptrueSelectButton : MonoBehaviour
{
    int index = -1;

    public void OnClick()
    {
        index = 0; ////// юс╫ц

        if (index == -1)
        {
            Debug.Log("Non index");
        }
        else
        {
            WorkUI.Instance.SelectSculpture(index);
        }
        AudioManager.Instance.PlayClick2Sound();

    }
}
