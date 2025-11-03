using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MineManager : MonoBehaviour
{
    public static MineManager Instance;

    Coroutine miningCoroutine;
    MineBase currentMine;
    GameObject RewardStone;
    bool isMining;
    public bool GetIsMining {  get { return isMining; } }

    [SerializeField] Cart cart;

    [SerializeField] GameObject LimestoneObject;


    private void Update()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        this.isMining = false;
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

    private IEnumerator StartMiningAfterGrounded(MineBase mine)
    {
        yield return StartCoroutine(PlayerMoveController.Instance.ApplyGravityUntilGrounded());

        // ¬¯¡ˆ »ƒ Mining Ω√¿€
        miningCoroutine = StartCoroutine(Mining(mine));
    }

    IEnumerator Mining(MineBase mine)
    {
        var pick = Pickaxe.Instance;

        if (PlayerManager.Instance.currentItem == null)
        {
            Debug.Log("√§±º«“ µµ±∏∏¶ µÈ∞Ì ¿÷¡ˆ æ ¿Ω");
            yield break;
        }

        Debug.Log("∞Ó±™¿Ã grade : " + PlayerManager.Instance.currentItem.grade);

        float pickSpeed = pick.GetPickaxeSpeed(PlayerManager.Instance.currentItem.grade);
        float d = mine.durability;
        Stone s = mine.GetStoneType();

        while (isMining)
        {
            yield return new WaitForSeconds(d / pickSpeed);
            // LimeStone¿« ∞ÊøÏ -> Wood 10√ , Stone 7.69....√ , Iron 5√ , Diamond 3.03..√ 

            if (!isMining)
            {
                break;
            }

            Debug.Log($"{mine.gameObject.name}ø°º≠ {s.stoneName} »πµÊ");

            Reward(mine);
        }
    }


    public void StopMining()
    {
        if (!isMining) return;

        isMining = false;

        if (currentMine != null)
        {
            currentMine.isBeingMined = false; // √§±º ¡æ∑· Ω√ ¿·±› «ÿ¡¶
            currentMine = null;
        }

        if (miningCoroutine != null)
            StopCoroutine(miningCoroutine);

        PlayerInteract.Instance.DeleteFocus();

        PlayerManager.Instance.ChangePlayerState(PlayerState.IDLE);

        Debug.Log("√§±º ¡ﬂ¥‹µ ");
    }    

    private void Reward(MineBase mine) // ∫∏ªÛ
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
