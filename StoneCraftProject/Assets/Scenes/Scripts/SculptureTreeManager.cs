using UnityEngine;

public class SculptureTreeManager : MonoBehaviour
{
    // 아이디어 재화로 Unlock 가능
    // 추후 스크립트 분리 & 개발 예정

    public static SculptureTreeManager Instance;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
