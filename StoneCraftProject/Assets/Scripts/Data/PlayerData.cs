using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int gold;

    public int hammerGrade;
    public int pickaxeGrade;

    public Vector3 pos;
    public string lastSceneName; // 현재 위치 저장
}