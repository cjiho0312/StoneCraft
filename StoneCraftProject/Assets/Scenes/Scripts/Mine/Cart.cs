using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cart : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject CreationArea;

    public List<int> stoneList; // īƮ�� ������ �ִ� �� ID ���

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
        // ���� ������ �������� �� (�տ� ������ �� ���)
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

        Debug.Log("���� ���� ����");
    }

    void StopPullCart()
    {
        if(blockInteract) return;

        transform.parent = null;

        isPulling = false;
        blockInteract = true;

        PlayerManager.Instance.ChangePlayerState(PlayerState.IDLE);
        StartCoroutine(BlockInteractShort());

        Debug.Log("���� ���� ��");
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
            stoneList.Add(s.GetStoneID()); // ����Ʈ�� �߰�
        }

        foreach (StoneObject s in Stones)
        {
            Destroy(s.gameObject); // ������Ʈ ����
        }
    }
}
