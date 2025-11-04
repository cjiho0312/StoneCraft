using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class StoneStorage : MonoBehaviour
{
    // 가진 돌 목록
    private int [] StoneReserves = new int[6];

    public void AddStones(List<int> list) // 받아온 list를 배열에 추가
    {
        foreach (int i in list)
        {
            StoneReserves[i - 101] += 1;
        }

        Debug.Log("Stone Storage 배열에 추가 완료");
    }

    public int [] GetStonesArray() // 돌 창고 배열 반환
    {
        return StoneReserves;
    }

    public void RemoveStone(int StoneID) // 배열에서 돌 삭제
    {
        if (StoneReserves[StoneID - 101] > 0)
        {
            StoneReserves[StoneID - 101] -= 1;
        }
    }
}
