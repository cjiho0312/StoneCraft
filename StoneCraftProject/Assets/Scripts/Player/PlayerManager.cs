using System.Linq.Expressions;
using UnityEngine;

public enum PlayerState
{
    IDLE,
    WALKING,
    RUNNING,
    JUMPING,
    MINNING,
    CRAFTING,
    NONE // Pause µî
}

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    public PlayerState currentState;

    PlayerMoveController playerMoveController;
    PlayerInteract playerInteract;
    Animator playerAnimator;

    public int money = 0;
    public int toolGrade = 0;
    public int pickaxeGrade = 0;


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
                    currentState = PlayerState.IDLE;
                    playerAnimator.SetInteger("PosMoveState", 0);
                    break;

                case PlayerState.WALKING:
                    playerInteract.enabled = true;
                    playerMoveController.isCanMove = true;
                    currentState = PlayerState.WALKING;
                    playerAnimator.SetInteger("PosMoveState", 1);
                    break;

                case PlayerState.RUNNING:
                    playerInteract.enabled = true;
                    playerMoveController.isCanMove = true;
                    currentState = PlayerState.RUNNING;
                    playerAnimator.SetInteger("PosMoveState", 2);
                    break;

                case PlayerState.JUMPING:
                    playerInteract.enabled = true;
                    playerMoveController.isCanMove = true;
                    currentState = PlayerState.JUMPING;
                    playerAnimator.SetTrigger("Jumping");
                    break;

                case PlayerState.MINNING:
                    playerInteract.enabled = false;
                    playerMoveController.isCanMove = false;
                    currentState = PlayerState.MINNING;
                    break;

                case PlayerState.CRAFTING:
                    playerInteract.enabled = false;
                    playerMoveController.isCanMove = false;
                    currentState = PlayerState.CRAFTING;
                    break;

                case PlayerState.NONE:
                    playerInteract.enabled = false;
                    playerMoveController.isCanMove = false;
                    currentState = PlayerState.NONE;
                    playerAnimator.SetInteger("PosMoveState", 0);
                    break;

                default:
                    break;

            }
        }
    }
}
