using System.Collections;
using UnityEngine;

public class MineManager : MonoBehaviour
{
    Coroutine miningCoroutine;
    bool isMining;
    MineBase currentMine;
    PauseUI pauseUI;
    GameObject RewardStone;

    [SerializeField] Cart cart;

    [SerializeField] GameObject LimestoneObject;


    private void Start()
    {
        this.isMining = false;
        pauseUI = PauseUI.Instance;
    }

    public void StartMining(MineBase mine)
    {
        var playerManager = PlayerManager.Instance;
        playerManager.ChangePlayerState(PlayerState.IDLE);

        if (this.isMining || mine.isBeingMined)
            return;
        
        isMining = true;

        AimSwitch.Instance.ChangeAim(AimState.NONE);

        currentMine = mine;
        currentMine.isBeingMined = true;

        playerManager.ChangePlayerState(PlayerState.MINING);

        StartCoroutine(StartMiningAfterGrounded(mine));
    }

    private void Update()
    {
        // ä�� ���� ���� �Է� ����
        if (isMining && !pauseUI.GetisOpenPauseMenu)
        {
            if (Input.GetKeyDown(KeyCode.E) ||
                Input.GetKeyDown(KeyCode.W) ||
                Input.GetKeyDown(KeyCode.A) ||
                Input.GetKeyDown(KeyCode.S) ||
                Input.GetKeyDown(KeyCode.D) ||
                Input.GetMouseButtonDown(0))
            {
                StopMining();
            }
        }
    }

    private IEnumerator StartMiningAfterGrounded(MineBase mine)
    {
        yield return StartCoroutine(PlayerMoveController.Instance.ApplyGravityUntilGrounded());

        // ���� �� Mining ����
        miningCoroutine = StartCoroutine(Mining(mine));
    }

    IEnumerator Mining(MineBase mine)
    {
        var pick = Pickaxe.Instance;

        if (PlayerManager.Instance.currentItem == null)
        {
            Debug.Log("ä���� ������ ��� ���� ����");
            yield break;
        }

        Debug.Log("��� grade : " + PlayerManager.Instance.currentItem.grade);

        float pickSpeed = pick.GetPickaxeSpeed(PlayerManager.Instance.currentItem.grade);
        float d = mine.durability;
        Stone s = mine.GetStoneType();

        while (isMining)
        {
            yield return new WaitForSeconds(d / pickSpeed);
            // LimeStone�� ��� -> Wood 10��, Stone 7.69....��, Iron 5��, Diamond 3.03..��

            if (!isMining)
            {
                break;
            }

            Debug.Log($"{mine.gameObject.name}���� {s.stoneName} ȹ��");

            Reward(mine);
        }
    }


    private void StopMining()
    {
        if (!isMining) return;

        isMining = false;

        if (currentMine != null)
        {
            currentMine.isBeingMined = false; // ä�� ���� �� ��� ����
            currentMine = null;
        }

        if (miningCoroutine != null)
            StopCoroutine(miningCoroutine);

        PlayerInteract.Instance.DeleteFocus();

        PlayerManager.Instance.ChangePlayerState(PlayerState.IDLE);

        Debug.Log("ä�� �ߴܵ�");
    }    

    private void Reward(MineBase mine) // ����
    {
        Vector3 CreationPos = cart.GetCreationAreaPos();
        int stoneId = mine.StoneType.stoneID;
        

        switch (stoneId)
        {
            case 101: // limestone
                RewardStone = LimestoneObject;
                break;

            case 102:
                break;

            default:
                break;

        }

        GameObject Stone = Instantiate(RewardStone, CreationPos, Quaternion.identity);
        Stone.transform.parent = cart.transform;
        Stone.SetActive(true);
    }

}
