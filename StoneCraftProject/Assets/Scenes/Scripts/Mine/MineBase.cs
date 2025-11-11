using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public abstract class MineBase : MonoBehaviour, IInteractable
{
    public abstract Stone StoneType { get; }
    public abstract float durability { get; } // 채석장 기본 내구도
    public bool isBeingMined = false;

    [SerializeField] public MineManager mineManager;
    

    public void OnFocus()
    {
        AimSwitch.Instance.ChangeAim(AimState.MINE);
    }

    public void OnLoseFocus()
    {
        AimSwitch.Instance.ChangeAim(AimState.NONE);
    }

    public abstract void OnInteract();
    
    public virtual Stone GetStoneType()
    {
        return StoneType;
    }
}
