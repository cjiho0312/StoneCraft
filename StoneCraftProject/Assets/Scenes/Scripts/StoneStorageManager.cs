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
        


    }
}
