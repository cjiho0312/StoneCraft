using UnityEngine;

public enum ItemType
{
    Pickaxe, // 곡괭이
    Tool, // 도구
    Bow, // 활
    Sculpture, // 조각품
    Loot // 사냥 전리품
}

[CreateAssetMenu(menuName = "Data/Item")]
public class Item : ScriptableObject
{
    public int itemId; // 고유 아이템 ID
    public string itemName; // 이름
    public ItemType itemtype; // 타입
    public Sprite itemImage; // 스프라이트
    public int grade;          // 도구 등급, 조각품 품질 등
    public int quantity = 1;   // 기본값 1 (도구는 1로 고정)
}
