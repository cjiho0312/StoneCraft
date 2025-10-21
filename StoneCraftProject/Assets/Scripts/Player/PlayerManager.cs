using System.Linq.Expressions;
using UnityEngine;

public enum PlayerState
{
    IDLE,
    WALKING,
    RUNNING,
    JUMPING,
    MINNING,
    CRAFTING
}

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    public PlayerState currentState;

    public PlayerData playerData { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        var saveManager = SaveManager.Instance;
        playerData = saveManager.savedPlayerData;

        currentState = PlayerState.IDLE;
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

    public void ChangePlayerState(PlayerState newState)
    {
        if (newState != currentState)
        {
            switch (newState)
            {
                case PlayerState.IDLE:
                    currentState = PlayerState.IDLE;
                    break;

                case PlayerState.WALKING:
                    currentState = PlayerState.WALKING;
                    break;

                case PlayerState.RUNNING:
                    currentState = PlayerState.RUNNING;
                    break;

                case PlayerState.JUMPING:
                    currentState = PlayerState.JUMPING;
                    break;

                case PlayerState.MINNING:
                    currentState = PlayerState.MINNING;
                    break;

                case PlayerState.CRAFTING:
                    currentState = PlayerState.CRAFTING;
                    break;

                default:
                    break;

            }
        }
    }
}
