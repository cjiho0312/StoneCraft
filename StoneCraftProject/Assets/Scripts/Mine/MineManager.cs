using System.Collections;
using UnityEngine;

public class MineManager : MonoBehaviour
{
    [SerializeField] Pickaxe pick;
    Coroutine miningCoroutine;
    bool isMining;
    MineBase currentMine;
    PauseUIManager pauseUI;

    private void Start()
    {
        this.isMining = false;
        pauseUI = PauseUIManager.Instance;
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
        // 채굴 중일 때만 입력 감시
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

        // 착지 후 Mining 시작
        miningCoroutine = StartCoroutine(Mining(mine));
    }

    IEnumerator Mining(MineBase mine)
    {
        float pickSpeed = pick.GetPickaxeSpeed(PlayerManager.Instance.pickaxeGrade);
        float d = mine.durability;
        StoneData s = mine.GetStoneType();

        while (isMining)
        {
            yield return new WaitForSeconds(d / pickSpeed);
            // LimeStone의 경우 -> Wood 10초, Stone 7.69....초, Iron 5초, Diamond 3.03..초

            if (!isMining)
            {
                break;
            }

            Debug.Log($"{mine.gameObject.name}에서 {s.stoneID} 획득");
            // 여따가 보상 추가
        }
    }

    private void StopMining()
    {
        if (!isMining) return;

        isMining = false;

        if (currentMine != null)
        {
            currentMine.isBeingMined = false; // 채굴 종료 시 잠금 해제
            currentMine = null;
        }

        if (miningCoroutine != null)
            StopCoroutine(miningCoroutine);

        PlayerInteract.Instance.DeleteFocus();

        PlayerManager.Instance.ChangePlayerState(PlayerState.IDLE);

        Debug.Log("채굴 중단됨");
    }    


}
