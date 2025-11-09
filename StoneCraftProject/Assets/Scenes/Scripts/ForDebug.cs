using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ForDebug : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) // µ¹ Ãß°¡
        {
            List <int> list = new List<int>();
            list.Add(101);
            StoneStorageManager.Instance.GetStonesInStorage(list);
        }
    }
}
