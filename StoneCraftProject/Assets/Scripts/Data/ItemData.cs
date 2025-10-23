
public enum ItemType
{
    Tool,       // 곡괭이, 조각 도구, 활 등
    Sculpture,  // 조각품
    Loot        // 사냥 전리품
}

[System.Serializable]
public class Item
{
    public int id;             // 고유 아이템 ID
    public string itemName;
    public ItemType type;
    public int grade;          // 도구 등급, 조각품 품질 등
    public int quantity = 1;   // 기본값 1 (도구는 1로 고정)
}
