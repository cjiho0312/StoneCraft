using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class StoneStorageManager : MonoBehaviour
{
    public static StoneStorageManager Instance;

    [SerializeField] StoneStorage stoneStorage;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void GetStonesInStorage(List <int> list)
    {
        // cart에서 가져온 ((int)StoneID)list를 StoneStorage에 옮기는 작업
    }
}
