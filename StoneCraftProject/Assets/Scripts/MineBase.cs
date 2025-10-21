using UnityEngine;

public abstract class MineBase : MonoBehaviour, IInteractable
{
    public abstract StoneData StoneType { get; }
    public abstract float durability { get; } // 채석장 기본 내구도
    [SerializeField] public MineManager mineManager;

    public void OnFocus()
    {
        Debug.Log("On Focus");
    }

    public void OnLoseFocus()
    {
        Debug.Log("Off Focus");
    }

    public abstract void OnInteract();
    
    public virtual StoneData GetStoneType()
    {
        Debug.Log($"Mining {StoneType.stoneID}");
        return StoneType;
    }
}
