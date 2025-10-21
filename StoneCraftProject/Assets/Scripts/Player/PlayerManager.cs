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
    public PlayerData playerData { get; private set; }

    PlayerMoveController playerMoveController;
    PlayerInteract playerInteract;


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
        SaveManager saveManager = SaveManager.Instance;

        if (saveManager != null)
            playerData = saveManager.savedPlayerData;
        else
            playerData = new PlayerData();

        playerMoveController = PlayerMoveController.Instance;
        playerInteract = PlayerInteract.Instance;

        currentState = PlayerState.IDLE;

        SetPlayerStateFromLoadData();
    }

    public bool SpendGold(int amount)
    {
        if (playerData.gold < amount) return false;
        playerData.gold -= amount;
        return true;
    }

    public void AddGold(int amount)
    {
        playerData.gold += amount;
    }

    public void SetPlayerStateFromLoadData()
    {
        SaveManager saveManager = SaveManager.Instance;

        Debug.Log("SetPlayerStateFromLoadData()");
        Debug.Log(saveManager.savedPlayerData.posX + " " +  saveManager.savedPlayerData.posY + " " + saveManager.savedPlayerData.posZ);

        ChangePlayerState(PlayerState.IDLE);
        PlayerMoveController.Instance.MoveTo(saveManager.savedPlayerData.posX, saveManager.savedPlayerData.posY, saveManager.savedPlayerData.posZ);
    }

    public void ApplyLoadedData()
    {
        SaveManager saveManager = SaveManager.Instance;

        playerData = SaveManager.Instance.savedPlayerData;

        if (PlayerMoveController.Instance != null)
            PlayerMoveController.Instance.MoveTo(saveManager.savedPlayerData.posX, saveManager.savedPlayerData.posY, saveManager.savedPlayerData.posZ);
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
                    break;

                case PlayerState.WALKING:
                    playerInteract.enabled = true;
                    playerMoveController.isCanMove = true;
                    currentState = PlayerState.WALKING;
                    break;

                case PlayerState.RUNNING:
                    playerInteract.enabled = true;
                    playerMoveController.isCanMove = true;
                    currentState = PlayerState.RUNNING;
                    break;

                case PlayerState.JUMPING:
                    playerInteract.enabled = true;
                    playerMoveController.isCanMove = true;
                    currentState = PlayerState.JUMPING;
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
                    break;

                default:
                    break;

            }
        }
    }
}
