using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public enum AimState
{
    NONE, // 기본 에임
    MINE, // 채석 에임
    SCULPT, // 조각 에임
    ELSE // 그 외 상호작용
}

public class AimSwitch : MonoBehaviour
{
    public static AimSwitch Instance;

    [Header("Aim UI Objects")]
    public GameObject aimNone;
    public GameObject aimMine;
    public GameObject aimSculpt;
    public GameObject aimElse;

    private Dictionary<AimState, GameObject> aimDictionary;
    private AimState currentAimState = AimState.NONE;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        aimDictionary = new Dictionary<AimState, GameObject>()
        {
            { AimState.NONE, aimNone },
            { AimState.MINE, aimMine },
            //{ AimState.SCULPT, aimSculpt },
            //{ AimState.ELSE, aimElse }
        };

        foreach (var kvp in aimDictionary)
            kvp.Value.SetActive(kvp.Key == AimState.NONE);
    }

    public void ChangeAim(AimState newState)
    {
        if (newState == currentAimState) return;

        if (aimDictionary.ContainsKey(currentAimState))
            aimDictionary[currentAimState].SetActive(false);

        if (aimDictionary.ContainsKey(newState))
            aimDictionary[newState].SetActive(true);

        currentAimState = newState;
    }
}
