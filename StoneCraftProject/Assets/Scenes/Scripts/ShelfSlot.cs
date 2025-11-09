using UnityEngine;
using UnityEngine.Rendering;
using static UnityEditor.Progress;

public class ShelfSlot : MonoBehaviour, IInteractable
{
    GameObject SculpturePrefab;
    Item SculptureData;



    private void Awake()
    {

    }


    public void OnFocus()
    {
        AimSwitch.Instance.ChangeAim(AimState.ELSE);
    }

    public void OnInteract()
    {
        if (SculpturePrefab == null)
        {
            if (PlayerManager.Instance.currentItem == null)
            {
                return;
            }
            else if (PlayerManager.Instance.currentItem.itemtype == ItemType.Sculpture)
            {
                DisplaySculptureOnSlot();
            }
            else
            {
                Debug.Log("조각품만 전시할 수 있습니다!");
                GuideTextManager.Instance.MakeGuide(GuideSub.GUIDE, "Only sculptures can be displayed!");
            }
        }
        else
        {
            if (Inventory.Instance.CanAddItem())
            {
                GetSculpturefromSlot();
            }
            else
            {
                Debug.Log("인벤토리가 가득 찼습니다");
                GuideTextManager.Instance.MakeGuide(GuideSub.GUIDE, "Inventory full!");
            }
        }

    }

    public void OnLoseFocus()
    {
        AimSwitch.Instance.ChangeAim(AimState.NONE);
    }


    void DisplaySculptureOnSlot() // 슬롯에 조각품 전시
    {
        // 전시 후
        GameObject prefab = PlayerManager.Instance.currentItem.holdingPrefab;
        SculpturePrefab = Instantiate(prefab, transform);
        SculpturePrefab.transform.localPosition = Vector3.zero;
        SculpturePrefab.transform.localScale = new Vector3 (30f, 30f, 30f);

        SculptureData = PlayerManager.Instance.currentItem;
        SculptureData.quantity = 1;

        // 인벤토리에서 아이템 삭제
        Inventory.Instance.RemoveSculptureItem(SculptureData.itemId);

    }

    void GetSculpturefromSlot() // 슬롯에 있던 조각품 가져오기
    {
        if (SculptureData == null)
        {
            Debug.LogWarning("SculptureData가 null입니다!");
            return;
        }

        Inventory.Instance.AddSculptureItem(SculptureData);

        Destroy(SculpturePrefab);
        SculpturePrefab = null;

        SculptureData = null;
    }

}
