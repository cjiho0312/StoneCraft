using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Cart : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject CreationArea;
    public Vector3 GetCreationAreaPos() { return CreationArea.transform.position; }

    private bool isPulling;
    private bool blockInteract;

    private void Awake()
    {
        isPulling = false;
        blockInteract = false;
    }

    void Update()
    {
        if (isPulling)
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
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

        Debug.Log("On Interact");
        // 추후 퀵슬롯 수정해햐 함 (손에 아이템 못 들게)
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

        Debug.Log("수레 끌기 끝");
    }

    IEnumerator BlockInteractShort()
    {
        yield return new WaitForSeconds(0.1f);
        blockInteract = false;
    }
}
