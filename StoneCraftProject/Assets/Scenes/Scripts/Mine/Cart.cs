using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cart : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject CreationArea;

    public List<int> stoneList; // 카트가 가지고 있는 돌 ID 목록

    private bool isPulling;
    private bool blockInteract;
    public Vector3 GetCreationAreaPos() { return CreationArea.transform.position; }

    private void Awake()
    {
        isPulling = false;
        blockInteract = false;
    }

    void Update()
    {
        if (isPulling)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                InitCartRot();
            }
            if (!Input.GetKey(KeyCode.E) && !Input.GetMouseButton(0))
            {
                StopPullCart();
            }
        }
    }

    public void OnFocus()
    {
        if (isPulling) { return; }

        Debug.Log("On Focus");
        AimSwitch.Instance.ChangeAim(AimState.ELSE);
    }

    public void OnInteract()
    {
        if (blockInteract || isPulling) { return; }

        if (!Input.GetKey(KeyCode.E) && !Input.GetMouseButton(0))
        { return; }

        Debug.Log("On Interact");
        StartPullCart();
    }

    public void OnLoseFocus()
    {
        if (isPulling) { return; }

        Debug.Log("Off Focus");
        AimSwitch.Instance.ChangeAim(AimState.NONE);
    }

    void StartPullCart()
    {
        if (blockInteract) return;
        PlayerManager.Instance.CanSeeHoldingTool(false);
        PlayerInteract.Instance.SetCanInteract(false);

        isPulling = true;
        blockInteract = true;

        AimSwitch.Instance.ChangeAim(AimState.NONE);

        var Player = PlayerMoveController.Instance;
        transform.parent = Player.transform;

        PlayerManager.Instance.ChangePlayerState(PlayerState.PULLINGCART);

        StartCoroutine(BlockInteractShort());

        Debug.Log("수레 끌기 시작");
    }

    void StopPullCart()
    {
        if(blockInteract) return;

        transform.parent = null;

        isPulling = false;
        blockInteract = true;

        PlayerManager.Instance.ChangePlayerState(PlayerState.IDLE);
        StartCoroutine(BlockInteractShort());

        PlayerManager.Instance.CanSeeHoldingTool(true);
        PlayerInteract.Instance.SetCanInteract(true);

        Debug.Log("수레 끌기 끝");
    }

    void InitCartRot() // 수레 세우기
    {
        StopPullCart();
        gameObject.transform.rotation = Quaternion.identity;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1, gameObject.transform.position.z);
    }

    IEnumerator BlockInteractShort()
    {
        yield return new WaitForSeconds(0.1f);
        blockInteract = false;
    }

    public void TakeStones()
    {
        var Stones = GetComponentsInChildren<StoneObject>();
        
        foreach (StoneObject s in Stones)
        {
            stoneList.Add(s.GetStoneID()); // 리스트에 추가
        }

        foreach (StoneObject s in Stones)
        {
            Destroy(s.gameObject); // 오브젝트 삭제
        }
    }

    public bool isHaveStones()
    {
        if (stoneList.Count == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void ClearList()
    {
        stoneList.Clear(); // 리스트 클리어
    }
}
