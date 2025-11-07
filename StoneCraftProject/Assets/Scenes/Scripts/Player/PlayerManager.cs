using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public enum PlayerState
{
    IDLE,
    WALKING,
    RUNNING,
    JUMPING,
    MINING,
    CRAFTING,
    PULLINGCART,
    NONE // Pause 등
}

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    public PlayerState currentState;

    PlayerMoveController playerMoveController;
    PlayerInteract playerInteract;
    Animator playerAnimator;
    public Animator ToolsAnimator;

    public int money = 0;

    public Item currentItem;
    public Transform handTransformForThirdP; // 3인칭용 손 위치
    public Transform handTransformForMe; // 1인칭용 손 위치

    private GameObject currentHandObject; // 손에 실제로 생성된 오브젝트

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        playerMoveController = PlayerMoveController.Instance;
        playerInteract = PlayerInteract.Instance;
        playerAnimator = playerMoveController.gameObject.GetComponent<Animator>();
    }
    public void SetCurrentItem(Item item)
    {
        currentItem = item;
        UpdateHandItem();
    }

    private void UpdateHandItem()
    {
        // 기존 손 오브젝트 제거
        if (currentHandObject != null)
        {
            Destroy(currentHandObject);
            currentHandObject = null;
        }

        if (currentItem != null && currentItem.holdingPrefab != null)
        {

            // 1인칭용 Prefab 생성
            currentHandObject = Instantiate(currentItem.holdingPrefab, handTransformForMe);
            currentHandObject.transform.localPosition = Vector3.zero;
            currentHandObject.transform.localRotation = Quaternion.identity;

            if (currentItem.itemtype == ItemType.Tool || currentItem.itemtype == ItemType.Pickaxe)
            {
                currentHandObject.transform.localScale = new Vector3(0.006f, 0.006f, 0.006f);
            }
            else if (currentItem.itemtype == ItemType.Sculpture)
            {
                currentHandObject.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            }

            if (currentItem.itemtype == ItemType.Pickaxe || currentItem.itemtype == ItemType.Tool)
                ToolsAnimator = currentHandObject.GetComponent<Animator>();

            UpdateToolAnimation();

            // // 멀티플레이어용 Prefab 생성
            // currentHandObject = Instantiate(currentItem.holdingPrefab, handTransformForThirdP);
            // currentHandObject.transform.localPosition = Vector3.zero;
            // currentHandObject.transform.localRotation = Quaternion.identity;
            // currentHandObject.transform.localScale = Vector3.one;
        }
    }

    public void CanSeeHoldingTool(bool Active)
    {
        if (currentHandObject == null) return;

        MeshRenderer ToolRenderer = currentHandObject.gameObject.GetComponentInChildren<MeshRenderer>();
        ToolRenderer.enabled = Active;
    }

    void UpdateToolAnimation()
    {
        if (currentState == PlayerState.WALKING)
        {
            ToolsAni("Walking");
        }
        else if (currentState == PlayerState.RUNNING)
        {
            ToolsAni("Running");
        }
    }

    private void ToolsAni(string Ani)
    {
        if (currentItem  == null) return;
        if (currentItem.itemtype != ItemType.Pickaxe && currentItem.itemtype != ItemType.Tool) { return; }

        AnimatorControllerParameter[] parameters = ToolsAnimator.parameters;

        foreach (AnimatorControllerParameter parameter in parameters)
        {
            // 파라미터 타입이 Bool일 경우에만 false로 설정
            if (parameter.type == AnimatorControllerParameterType.Bool)
            {
                ToolsAnimator.SetBool(parameter.name, false);
            }
        }

        if (Ani == "N") { return; }

        ToolsAnimator.SetBool(Ani, true);
}

    public bool SpendMoney(int amount)
    {
        if (money < amount) return false;
        money -= amount;
        return true;
    }

    public void AddMoney(int amount)
    {
        money += amount;
    }

    public void ChangePlayerState(PlayerState newState)
    {
        if (newState != currentState)
        {
            switch (newState)
            {
                case PlayerState.IDLE:
                    playerInteract.enabled = true;
                    playerMoveController.isCanMove = true;
                    playerAnimator.SetBool("Mining", false);
                    ToolsAni("N");
                    currentState = PlayerState.IDLE;
                    playerAnimator.SetInteger("PosMoveState", 0);
                    break;

                case PlayerState.WALKING:
                    playerInteract.enabled = true;
                    playerMoveController.isCanMove = true;
                    playerAnimator.SetBool("Mining", false);
                    ToolsAni("Walking");
                    currentState = PlayerState.WALKING;
                    playerAnimator.SetInteger("PosMoveState", 1);
                    break;

                case PlayerState.RUNNING:
                    playerInteract.enabled = true;
                    playerMoveController.isCanMove = true;
                    playerAnimator.SetBool("Mining", false);
                    ToolsAni("Running");
                    currentState = PlayerState.RUNNING;
                    playerAnimator.SetInteger("PosMoveState", 2);
                    break;

                case PlayerState.JUMPING:
                    playerInteract.enabled = true;
                    playerMoveController.isCanMove = true;
                    currentState = PlayerState.JUMPING;
                    playerAnimator.SetTrigger("Jumping");
                    ToolsAni("N");
                    break;

                case PlayerState.MINING:
                    playerInteract.enabled = false;
                    playerMoveController.isCanMove = false;
                    currentState = PlayerState.MINING;
                    ToolsAni("Acting");
                    playerAnimator.SetBool("Mining", true);
                    break;

                case PlayerState.CRAFTING:
                    playerInteract.enabled = false;
                    playerMoveController.isCanMove = false;
                    playerAnimator.SetBool("Mining", false);
                    ToolsAni("N");
                    currentState = PlayerState.CRAFTING;
                    break;

                case PlayerState.PULLINGCART:
                    playerInteract.enabled = false;
                    playerMoveController.isCanMove = true;
                    playerAnimator.SetBool("Mining", false);
                    ToolsAni("N");
                    currentState = PlayerState.PULLINGCART;
                    break;

                case PlayerState.NONE:
                    playerInteract.enabled = false;
                    playerMoveController.isCanMove = false;
                    playerAnimator.SetBool("Mining", false);
                    ToolsAni("N");
                    currentState = PlayerState.NONE;
                    playerAnimator.SetInteger("PosMoveState", 0);
                    break;

                default:
                    break;

            }
        }
    }
}
