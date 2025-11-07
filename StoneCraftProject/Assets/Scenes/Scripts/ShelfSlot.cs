using UnityEngine;
using UnityEngine.Rendering;
using static UnityEditor.Progress;

public class ShelfSlot : MonoBehaviour, IInteractable
{
    GameObject Sculpture;
    Collider col;


    private void Awake()
    {
        col = GetComponent<Collider>();
    }


    public void OnFocus()
    {
        AimSwitch.Instance.ChangeAim(AimState.ELSE);
    }

    public void OnInteract()
    {
        if (PlayerManager.Instance.currentItem.itemtype == ItemType.Sculpture)
        {
            if (Sculpture == null)
            {
                DisplaySculptureOnSlot();
            }
            else
            {
                GetSculpturefromSlot();
            }
        }
        else
        {
            Debug.Log("조각품만 전시할 수 있습니다!");
        }

    }

    public void OnLoseFocus()
    {
        AimSwitch.Instance.ChangeAim(AimState.NONE);
    }


    void DisplaySculptureOnSlot() // 슬롯에 조각품 전시
    {
        // 전시 후

        // 인벤토리에서 아이템 삭제
        int itemid = PlayerManager.Instance.currentItem.itemId;
        Inventory.Instance.RemoveItem(itemid, 1);

    }

    void GetSculpturefromSlot() // 슬롯에 있던 조각품 가져오기
    {

    }

}
