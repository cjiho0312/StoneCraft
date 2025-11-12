using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ForDebug : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKey(KeyCode.Q) && Input.GetKeyDown(KeyCode.Alpha1)) // µ¹ Ãß°¡
        {
            List <int> list = new List<int>();
            list.Add(101);
            StoneStorageManager.Instance.GetStonesInStorage(list);
        }
        if (Input.GetKey(KeyCode.Q) && Input.GetKeyDown(KeyCode.Alpha2))
        {
            List<int> list = new List<int>();
            list.Add(102);
            StoneStorageManager.Instance.GetStonesInStorage(list);
        }
    }
}
