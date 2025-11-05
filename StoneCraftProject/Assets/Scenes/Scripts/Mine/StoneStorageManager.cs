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

    private void Start()
    {
        // savedata에서 StoneStorage 배열 받아서 집어넣기
    }

    public void GetStonesInStorage(List <int> list)
    {
        stoneStorage.AddStones(list);
    }

    public void RemoveStoneInStorage(int StoneID)
    {
        stoneStorage.RemoveStone(StoneID);
    }

    public int [] GetStonesArrayfromStorage()
    {
        return stoneStorage.GetStonesArray();
    }
}
