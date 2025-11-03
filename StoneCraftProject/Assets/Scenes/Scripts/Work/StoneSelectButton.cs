using System;
using UnityEngine;

public class StoneSelectButton : MonoBehaviour
{
    int index = -1;

    public void OnClick()
    {
        if (index == -1)
        {
            Debug.Log("Non index");
        }
        else
        {
            WorkUI.Instance.SelectStone(index);
        }
    }

    public void SaveStoneIndex(int i)
    {
        index = i;
        Debug.Log("SaveStone Index : " + index);
    }
}
