using System.Collections;
using UnityEngine;

public class MineManager : MonoBehaviour
{
    [SerializeField] Pickaxe pick;
    Animator pickAnimator;
    Coroutine miningCoroutine;
    bool isMining;
    MineBase currentMine;

    private void Start()
    {
        pickAnimator = pick.gameObject.GetComponent<Animator>();
        this.isMining = false;
    }

    public void StartMining(MineBase mine)
    {
        if (this.isMining || mine.isBeingMined)
            return;
        
        isMining = true;

        AimSwitch.Instance.ChangeAim(AimState.NONE);

        currentMine = mine;
        currentMine.isBeingMined = true;

        var playerManager = PlayerManager.Instance;
        float d = mine.durability;
        StoneData s = mine.GetStoneType();
        playerManager.ChangePlayerState(PlayerState.MINNING);

        miningCoroutine = StartCoroutine(Mining(mine));
    }

    private void Update()
    {
        // √§±º ¡ﬂ¿œ ∂ß∏∏ ¿‘∑¬ ∞®Ω√
        if (isMining)
        {
            if (Input.GetKeyDown(KeyCode.E) ||
                Input.GetKeyDown(KeyCode.W) ||
                Input.GetKeyDown(KeyCode.A) ||
                Input.GetKeyDown(KeyCode.S) ||
                Input.GetKeyDown(KeyCode.D))
            {
                StopMining();
            }
        }
    }

    IEnumerator Mining(MineBase mine)
    {
        float pickSpeed = pick.GetPickaxeSpeed(PlayerManager.Instance.playerData.pickaxeGrade);

        pickAnimator.SetBool("isMine", true);

        while (isMining)
        {
            yield return new WaitForSeconds(3f);

            if (!isMining)
            {
                break;
            }

            Debug.Log("mineø°º≠ ±§ºÆ »πµÊ");
        }
    }

    private void StopMining()
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

        pickAnimator.SetBool("isMine", false);

        PlayerInteract.Instance.DeleteFocus();

        PlayerManager.Instance.ChangePlayerState(PlayerState.IDLE);

        Debug.Log("√§±º ¡ﬂ¥‹µ ");
    }    


}
